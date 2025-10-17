using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ContentService.Web.Api.Endpoints;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ContentService.Web.Api.Extensions;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public static class EndpointExtensions
{
	public static IServiceCollection AddEndpoints(
		this IServiceCollection services,
		Assembly assembly)
	{
		ArgumentNullException.ThrowIfNull(assembly);
		
		var serviceDescriptors = assembly
			.DefinedTypes
			.Where(type => type is { IsAbstract: false, IsInterface: false } &&
			               type.IsAssignableTo(typeof(IEndpoint)))
			.Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
			.ToArray();
		
		services.TryAddEnumerable(serviceDescriptors);

		return services;
	}

	public static IApplicationBuilder MapEndpoints(
		this WebApplication app,
		RouteGroupBuilder? routeGroupBuilder = null)
	{
		ArgumentNullException.ThrowIfNull(routeGroupBuilder);
		ArgumentNullException.ThrowIfNull(app);
		
		var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

		IEndpointRouteBuilder builder = routeGroupBuilder;
		
		foreach (var endpoint in endpoints)
		{
			endpoint.MapEndpoint(builder);
		}
		
		return app;
	}
}