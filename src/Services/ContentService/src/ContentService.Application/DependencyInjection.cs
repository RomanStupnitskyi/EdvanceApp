using ContentService.Application.Abstractions.Behaviors;
using ContentService.Application.Messaging;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace ContentService.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.Scan(scan => scan.FromAssembliesOf(typeof(DependencyInjection))
			.AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
				.AsImplementedInterfaces()
				.WithScopedLifetime()
			.AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
				.AsImplementedInterfaces()
				.WithScopedLifetime()
			.AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
				.AsImplementedInterfaces()
				.WithScopedLifetime()
		);

		services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));
		services.Decorate(typeof(ICommandHandler<>), typeof(ValidationDecorator.CommandBaseHandler<>));
		
		services.Decorate(typeof(IQueryHandler<,>), typeof(LoggingDecorator.QueryHandler<,>));
		services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandHandler<,>));
		services.Decorate(typeof(ICommandHandler<>), typeof(LoggingDecorator.CommandBaseHandler<>));

		services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

		return services;
	}

	public static IBusRegistrationConfigurator AddApplicationConsumers(
		this IBusRegistrationConfigurator configurator)
	{
		configurator.AddConsumers(typeof(DependencyInjection).Assembly);
		
		return configurator;
	}
}