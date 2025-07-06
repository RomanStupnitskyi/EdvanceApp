using ContentService.Application.Messaging;
using ContentService.Application.Submissions.Delete;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Submissions;

public class Delete : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapDelete("submissions/{submissionId:guid}", async (
			Guid submissionId,
			ICommandHandler<DeleteSubmissionCommand> handler,
			CancellationToken cancellationToken) =>
		{
			var command = new DeleteSubmissionCommand(submissionId);

			var result = await handler.Handle(command, cancellationToken);

			return result.Match(Results.NoContent, CustomResults.Problem);
		})
		.WithTags(Tags.Submissions);
	}
}