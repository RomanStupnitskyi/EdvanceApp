﻿using ContentService.Application.Assignments.Update;
using ContentService.Application.Messaging;
using ContentService.Domain.Assignments.DTOs;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Assignments;

public class Update : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapPut("/assignments/{assignmentId:guid}", async (
			Guid assignmentId,
			UpdateAssignmentDto dto,
			ICommandHandler<UpdateAssignmentCommand, UpdateAssignmentResponse> handler,
			CancellationToken cancellationToken) =>
		{
			var command = new UpdateAssignmentCommand
			{
				AssignmentId = assignmentId,
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