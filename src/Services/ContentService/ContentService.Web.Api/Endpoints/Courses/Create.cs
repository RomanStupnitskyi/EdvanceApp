using ContentService.Application.Courses.Create;
using ContentService.Application.Messaging;
using ContentService.Domain.Courses.DTOs;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Courses;

public class Create : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost("courses", async (
			CreateCourseDto dto,
			ICommandHandler<CreateCourseCommand, CreateCourseResponse> handler,
			CancellationToken cancellationToken) =>
			{
				var command = new CreateCourseCommand
				{
					Title = dto.Title,
					Description = dto.Description,
					IsVisible = dto.IsVisible,
					CreatedBy = Guid.Empty // TODO: Implement user context to get the actual user ID
				};
				
				var result = await handler.Handle(command, cancellationToken);

				return result.Match(Results.Ok, CustomResults.Problem);
			})
		.WithTags(Tags.Courses);
	}
}