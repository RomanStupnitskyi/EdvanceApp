using ContentService.Application.Assignments.GetByCourseId;
using ContentService.Application.Messaging;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Assignments;

public class GetByCourseId : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("courses/{courseId:guid}/assignments", async (
			Guid courseId,
			IQueryHandler<GetAssignmentByCourseIdQuery, List<CourseAssignmentResponse>> getAssignmentsHandler,
			CancellationToken cancellationToken) =>
		{
			var query = new GetAssignmentByCourseIdQuery(courseId);

			var result = await getAssignmentsHandler.Handle(query, cancellationToken);

			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Assignments);
	}
}