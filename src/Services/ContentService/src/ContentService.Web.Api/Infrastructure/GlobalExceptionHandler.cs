using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ContentService.Web.Api.Infrastructure;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
	: IExceptionHandler
{
	
	private static readonly Action<ILogger, string, Exception?> LogException =
		LoggerMessage.Define<string>(
			LogLevel.Information,
			new EventId(1, nameof(LogException)),
			"Unhandled exception occurred {Exception}");
	
	public async ValueTask<bool> TryHandleAsync(
		HttpContext httpContext,
		Exception exception,
		CancellationToken cancellationToken)
	{
		LogException(logger, exception.ToString(), null);

		var problemDetails = new ProblemDetails
		{
			Status = StatusCodes.Status500InternalServerError,
			Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
			Title = "Server failure"
		};

		httpContext.Response.StatusCode = problemDetails.Status.Value;

		await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

		return true;
	}
}
