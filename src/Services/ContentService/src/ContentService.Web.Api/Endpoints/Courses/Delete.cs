using System.Diagnostics.CodeAnalysis;
using ContentService.Application.Courses.Delete;
using ContentService.Application.Messaging;
using ContentService.SharedKernel;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Courses;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public class Delete : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapDelete("courses/{courseId:guid}", async (
			Guid courseId,
			ICommandHandler<DeleteCourseCommand> handler,
			CancellationToken cancellationToken) =>
		{
			var command = new DeleteCourseCommand(courseId);
		
			Result result = await handler.Handle(command, cancellationToken);
		
			return result.Match(Results.NoContent, CustomResults.Problem);
		})
		.WithTags(Tags.Courses);
	}
}
