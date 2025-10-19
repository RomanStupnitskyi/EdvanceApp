using System.Diagnostics.CodeAnalysis;
using ContentService.Application.Messaging;
using ContentService.Application.Submissions.Update;
using ContentService.Domain.AssignmentSubmissions.DTOs;
using ContentService.SharedKernel;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Submissions;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public class Update : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPut("/submissions/{submissionId:guid}", async (
			Guid submissionId,
			UpdateSubmissionDto dto,
			ICommandHandler<UpdateSubmissionCommand, UpdateSubmissionResponse> handler,
			CancellationToken cancellationToken) =>
		{
			var command = new UpdateSubmissionCommand(submissionId)
			{
				Content = dto.Content
			};
			
			Result<UpdateSubmissionResponse> result = await handler.Handle(command, cancellationToken);

			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Submissions);
	}
}
