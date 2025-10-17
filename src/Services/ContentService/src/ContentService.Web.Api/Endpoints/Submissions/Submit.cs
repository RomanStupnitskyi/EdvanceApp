using System.Diagnostics.CodeAnalysis;
using ContentService.Application.Messaging;
using ContentService.Application.Submissions.SubmitAssignment;
using ContentService.Domain.AssignmentSubmissions.DTOs;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Submissions;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public class Submit : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPost("assignments/{assignmentId:guid}/submissions", async (
			Guid assignmentId,
			SubmitAssignmentDto dto,
			ICommandHandler<SubmitAssignmentCommand, AssignmentSubmittedResponse> handler,
			CancellationToken cancellationToken) =>
		{
			var command = new SubmitAssignmentCommand
			{
				AssignmentId = assignmentId,
				StudentId = Guid.NewGuid(),
				Content = dto.Content
			};

			var result = await handler.Handle(command, cancellationToken).ConfigureAwait(false);

			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Submissions);
	}
}