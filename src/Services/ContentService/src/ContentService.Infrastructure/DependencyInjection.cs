using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using ContentService.Application.Abstractions.Data;
using Ardalis.Specification;
using ContentService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContentService.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration configuration) =>
		services
			.AddDatabase(configuration)
			.AddHybridCaching(configuration)
			.AddHealthChecks(configuration);
	
	public static IOrderedSpecificationBuilder<T> OrderByDirection<T>(
		this ISpecificationBuilder<T> builder,
		Expression<Func<T,object?>> expression,
		bool ascending = false)
	{
		return ascending switch
		{
			true => builder.OrderBy(expression),
			false => builder.OrderByDescending(expression)
		};
	}
	
	private static IServiceCollection AddDatabase(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		string? connectionString = configuration.GetConnectionString("DefaultConnection");

		services.AddDbContext<ApplicationDbContext>(
			options => options
				.UseNpgsql(connectionString, npgsqlOptions =>
					npgsqlOptions.MigrationsHistoryTable(
						HistoryRepository.DefaultTableName,
						Schemas.Default))
				.UseSnakeCaseNamingConvention());

		services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

		return services;
	}

	private static IServiceCollection AddHybridCaching(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddHybridCache(options =>
		{
			options.MaximumPayloadBytes = 1024 * 1024;
			options.MaximumKeyLength = 1024;
			options.DefaultEntryOptions = new HybridCacheEntryOptions
			{
				Expiration = TimeSpan.FromMinutes(15), // L2 cache expiration
				LocalCacheExpiration = TimeSpan.FromMinutes(5) // L1 cache expiration
			};
		});

		// Configure Redis as L2 distributed cache
		services.AddStackExchangeRedisCache(options =>
		{
			options.Configuration = configuration.GetConnectionString("RedisConnectionString")
				?? throw new InvalidOperationException("Redis connection string is not configured.");
			options.InstanceName = "EdvanceApp.ContentService:";
			options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
			{
				EndPoints = { options.Configuration },
				AbortOnConnectFail = false,
				ConnectRetry = 3,
				ConnectTimeout = 5000
			};
		});

		return services;
	}
	
	[SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
	private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.AddHealthChecks()
			.AddNpgSql(configuration.GetConnectionString("DefaultConnection")!);

		return services;
	}
}
