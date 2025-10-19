using System.Diagnostics.CodeAnalysis;
using ContentService.Application.Courses.Update;
using ContentService.Application.Messaging;
using ContentService.Domain.Courses.DTOs;
using ContentService.SharedKernel;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Courses;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public class Update : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPut("courses/{courseId:Guid}", async (
			Guid courseId,
			UpdateCourseDto dto,
			ICommandHandler<UpdateCourseCommand, UpdateCourseResponse> handler,
			CancellationToken cancellationToken) =>
		{
			var command = new UpdateCourseCommand(courseId)
			{
				Title = dto.Title,
				Description = dto.Description,
				IsVisible = dto.IsVisible
			};

			Result<UpdateCourseResponse> result = await handler.Handle(command, cancellationToken);

			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Courses);
	}
}
