﻿using ContentService.Application.Assignments.Delete;
using ContentService.Application.Messaging;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Assignments;

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
		
			var result = await handler.Handle(command, cancellationToken);
		
			return result.Match(Results.NoContent, CustomResults.Problem);
		})
		.WithTags(Tags.Assignments);
	}
}