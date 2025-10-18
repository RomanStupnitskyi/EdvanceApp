using System.Diagnostics.CodeAnalysis;
using ContentService.Application.Courses.GetById;
using ContentService.Application.Messaging;
using ContentService.SharedKernel;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Courses;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public class GetById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("courses/{courseId:guid}", async (
			Guid courseId,
			IQueryHandler<GetCourseByIdQuery, CourseByIdResponse> handler,
			CancellationToken cancellationToken) =>
		{
			var query = new GetCourseByIdQuery(courseId);
		
			Result<CourseByIdResponse> result = await handler.Handle(query, cancellationToken).ConfigureAwait(false);
		
			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Courses);
	}
}