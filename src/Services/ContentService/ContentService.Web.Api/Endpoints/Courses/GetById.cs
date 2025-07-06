using ContentService.Application.Courses.GetById;
using ContentService.Application.Messaging;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Courses;

public class GetById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("courses/{courseId:guid}", async (
			Guid courseId,
			IQueryHandler<GetCourseByIdQuery, CourseResponse> handler,
			CancellationToken cancellationToken) =>
		{
			var query = new GetCourseByIdQuery(courseId);
		
			var result = await handler.Handle(query, cancellationToken);
		
			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Courses);
	}
}