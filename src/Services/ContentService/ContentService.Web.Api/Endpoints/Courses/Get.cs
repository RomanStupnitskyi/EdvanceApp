using ContentService.Application.Courses.Get;
using ContentService.Application.Messaging;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Courses;

public class Get : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("courses", async (
			IQueryHandler<GetCoursesQuery, List<GetCourseResponse>> handler,
			CancellationToken cancellationToken) =>
		{
			var query = new GetCoursesQuery();
		
			var result = await handler.Handle(query, cancellationToken);

			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Courses);
	}
}