using ContentService.Application.Messaging;
using ContentService.Application.Submissions.Get;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Submissions;

public class Get : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("submissions", async (
			IQueryHandler<GetSubmissionsQuery, List<GetSubmissionResponse>> handler,
			CancellationToken cancellationToken) =>
		{
			var query = new GetSubmissionsQuery();
			
			var result  = await handler.Handle(query, cancellationToken);

			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Submissions);
	}
}