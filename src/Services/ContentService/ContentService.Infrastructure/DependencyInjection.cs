using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using ContentService.Application.Abstractions.Data;
using Ardalis.Specification;
using ContentService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContentService.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration configuration) =>
		services
			.AddServices()
			.AddDatabase(configuration)
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
	
	private static IServiceCollection AddServices(this IServiceCollection services)
	{
		// services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
		//
		// services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();

		return services;
	}
	
	private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection");

		services.AddDbContext<ApplicationDbContext>(
			options => options
				.UseNpgsql(connectionString, npgsqlOptions =>
					npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
				.UseSnakeCaseNamingConvention());

		services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

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