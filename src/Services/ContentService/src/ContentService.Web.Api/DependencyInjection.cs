using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using ContentService.Application;
using ContentService.Web.Api.Infrastructure;
using MassTransit;

namespace ContentService.Web.Api;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public static class DependencyInjection
{
	public static IServiceCollection AddPresentation(
		this IServiceCollection services)
	{
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		services.AddExceptionHandler<GlobalExceptionHandler>();
		services.AddProblemDetails();

		return services;
	}
	
	public static IServiceCollection AddMassTransit(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddMassTransit(configurator =>
		{
			configurator.SetKebabCaseEndpointNameFormatter();

			configurator
				.AddApplicationConsumers()
				.UsingRabbitMq((context, factoryConfigurator) =>
					{
						factoryConfigurator.Host(configuration["MessageBroker:Hostname"]
								?? throw new InvalidOperationException("RabbitMQ Hostname is not configured"), 
							ushort.Parse(configuration["MessageBroker:Port"] ?? "5672", CultureInfo.InvariantCulture), 
							"/",
							hostConfigurator =>
							{
								hostConfigurator.Username(configuration["MessageBroker:Username"]
								    ?? throw new InvalidOperationException("RabbitMQ Username is not configured"));
								hostConfigurator.Password(configuration["MessageBroker:Password"]
								    ?? throw new InvalidOperationException("RabbitMQ Password is not configured"));
							});
					
						factoryConfigurator.ConfigureEndpoints(context);
					});
		});

		return services;
	}
}