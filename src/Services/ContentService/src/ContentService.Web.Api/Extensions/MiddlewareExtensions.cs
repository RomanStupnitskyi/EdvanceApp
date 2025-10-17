using System.Diagnostics.CodeAnalysis;
using ContentService.Web.Api.Middleware;

namespace ContentService.Web.Api.Extensions;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public static class MiddlewareExtensions
{
	public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
	{
		app.UseMiddleware<RequestContextLoggingMiddleware>();

		return app;
	}
}