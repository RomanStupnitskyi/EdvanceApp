using System.Diagnostics.CodeAnalysis;
using ContentService.Application.Messaging;
using ContentService.Application.Submissions.GetMany;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Submissions;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public class GetMany : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("submissions", async (
			IQueryHandler<GetSubmissionsQuery, List<GetSubmissionResponse>> handler,
			CancellationToken cancellationToken) =>
		{
			var query = new GetSubmissionsQuery();
			
			var result  = await handler.Handle(query, cancellationToken).ConfigureAwait(false);

			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Submissions);
	}
}