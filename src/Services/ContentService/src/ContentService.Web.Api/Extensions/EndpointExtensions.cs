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
		ServiceDescriptor[] serviceDescriptors = [.. assembly
			.DefinedTypes
			.Where(type => type is { IsAbstract: false, IsInterface: false } &&
			               type.IsAssignableTo(typeof(IEndpoint)))
			.Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))];
		
		services.TryAddEnumerable(serviceDescriptors);

		return services;
	}

	public static IApplicationBuilder MapEndpoints(
		this WebApplication app,
		RouteGroupBuilder? routeGroupBuilder = null)
	{
		
		IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

		IEndpointRouteBuilder builder = (IEndpointRouteBuilder)routeGroupBuilder ?? app;
		
		foreach (IEndpoint endpoint in endpoints)
		{
			endpoint.MapEndpoint(builder);
		}
		
		return app;
	}
}
