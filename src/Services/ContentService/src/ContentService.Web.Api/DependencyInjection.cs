using ContentService.Application;
using ContentService.Web.Api.Infrastructure;
using MassTransit;

namespace ContentService.Web.Api;

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
								?? throw new Exception("RabbitMQ Hostname is not configured"),
							ushort.Parse(configuration["MessageBroker:Port"] ?? "5672"),
							"/",
							hostConfigurator =>
							{
								hostConfigurator.Username(configuration["MessageBroker:Username"]
								    ?? throw new Exception("RabbitMQ Username is not configured"));
								hostConfigurator.Password(configuration["MessageBroker:Password"]
								    ?? throw new Exception("RabbitMQ Password is not configured"));
							});
					
						factoryConfigurator.ConfigureEndpoints(context);
					});
		});

		return services;
	}
}