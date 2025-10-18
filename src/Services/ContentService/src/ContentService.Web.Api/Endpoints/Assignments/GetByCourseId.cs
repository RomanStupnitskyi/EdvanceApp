using System.Diagnostics.CodeAnalysis;
using ContentService.Application.Assignments.GetByCourseId;
using ContentService.Application.Messaging;
using ContentService.SharedKernel;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Assignments;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
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

			Result<List<CourseAssignmentResponse>> result = await getAssignmentsHandler.Handle(query, cancellationToken).ConfigureAwait(false);

			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Assignments);
	}
}