using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace ContentService.Web.Api.Middleware;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public class RequestContextLoggingMiddleware(RequestDelegate next)
{
	private const string CorrelationIdHeaderName = "Correlation-Id";

	public Task Invoke(HttpContext context)
	{
		using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
		{
			return next.Invoke(context);
		}
	}

	private static string GetCorrelationId(HttpContext context)
	{
		context.Request.Headers.TryGetValue(
			CorrelationIdHeaderName,
			out StringValues correlationId);

		return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
	}
}
