using System.Diagnostics.CodeAnalysis;
using ContentService.Application.Courses.GetMany;
using ContentService.Application.Messaging;
using ContentService.SharedKernel;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Courses;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public class GetMany : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("courses", async (
			IQueryHandler<GetCoursesQuery, List<GetCourseResponse>> handler,
			CancellationToken cancellationToken) =>
		{
			var query = new GetCoursesQuery();
		
			Result<List<GetCourseResponse>> result = await handler.Handle(query, cancellationToken);

			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Courses);
	}
}
