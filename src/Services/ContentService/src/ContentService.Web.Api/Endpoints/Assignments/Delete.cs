using System.Diagnostics.CodeAnalysis;
using ContentService.Application.Assignments.Delete;
using ContentService.Application.Messaging;
using ContentService.SharedKernel;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Assignments;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public class Delete : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapDelete("/assignments/{assignmentId:guid}", async (
			Guid assignmentId,
			ICommandHandler<DeleteAssignmentCommand> handler,
			CancellationToken cancellationToken) =>
		{
			var command = new DeleteAssignmentCommand(assignmentId);
		
			Result result = await handler.Handle(command, cancellationToken);
		
			return result.Match(Results.NoContent, CustomResults.Problem);
		})
		.WithTags(Tags.Assignments);
	}
}
