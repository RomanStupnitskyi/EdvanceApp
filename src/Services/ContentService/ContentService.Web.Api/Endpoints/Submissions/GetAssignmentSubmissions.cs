using ContentService.Application.Messaging;
using ContentService.Application.Submissions.GetByAssignmentId;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Submissions;

public class GetAssignmentSubmissions : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("assignments/{assignmentId:guid}/submissions", async (
			Guid assignmentId,
			IQueryHandler<GetSubmissionsByAssignmentIdQuery, List<AssignmentSubmissionResponse>> handler,
			CancellationToken cancellationToken) =>
		{
			var query = new GetSubmissionsByAssignmentIdQuery(assignmentId);
			
			var result = await handler.Handle(query, cancellationToken);

			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Submissions);
	}
}