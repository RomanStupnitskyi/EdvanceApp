using ContentService.Application.Assignments.Create;
using ContentService.Application.Courses.GetById;
using ContentService.Application.Messaging;
using ContentService.Domain.Assignments.DTOs;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Assignments;

public class Create : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost("courses/{courseId:guid}/assignments", async (
			Guid courseId,
			CreateAssignmentDto dto,
			ICommandHandler<CreateAssignmentCommand, CreateAssignmentResponse> handler,
			CancellationToken cancellationToken) =>
		{
			var command = new CreateAssignmentCommand
			{
				CourseId = courseId,
				Title = dto.Title,
				Description = dto.Description,
				AllowLateSubmissions = dto.AllowLateSubmissions,
				AllowResubmissions = dto.AllowResubmissions,
				IsVisible = dto.IsVisible,
				StartDate = dto.StartDate,
				EndDate = dto.EndDate
			};

			var result = await handler.Handle(command, cancellationToken);

			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Assignments);
	}
}