﻿using Serilog.Context;

namespace ContentService.Web.Api.Middleware;

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
			out var correlationId);

		return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
	}
}