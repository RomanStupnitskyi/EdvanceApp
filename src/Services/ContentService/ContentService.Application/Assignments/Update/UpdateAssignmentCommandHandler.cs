using ContentService.Domain.Assignments;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Application.Assignments.Update;

public class UpdateAssignmentCommandHandler(IApplicationDbContext dbContext)
	: ICommandHandler<UpdateAssignmentCommand, UpdateAssignmentResponse>
{
	public async Task<Result<UpdateAssignmentResponse>> Handle(
		UpdateAssignmentCommand command,
		CancellationToken cancellationToken)
	{
		var assignment = await dbContext.Assignments
			.SingleOrDefaultAsync(assignment => assignment.Id == command.AssignmentId, cancellationToken);

		if (assignment == null)
			return Result.Failure<UpdateAssignmentResponse>(AssignmentErrors.NotFound(command.AssignmentId));

		if (command.Title != assignment.Title && command.Title != null)
			assignment.Title = command.Title;

		if (command.Description != assignment.Description && command.Description != null)
			assignment.Description = command.Description;

		if (command.AllowLateSubmissions != assignment.AllowLateSubmissions && command.AllowLateSubmissions.HasValue)
			assignment.AllowLateSubmissions = command.AllowLateSubmissions.Value;

		if (command.AllowResubmissions != assignment.AllowResubmissions && command.AllowResubmissions.HasValue)
			assignment.AllowResubmissions = command.AllowResubmissions.Value;

		if (command.IsVisible != assignment.IsVisible && command.IsVisible.HasValue)
			assignment.IsVisible = command.IsVisible.Value;

		if (command.StartDate.HasValue)
			assignment.StartDate = command.StartDate.Value;

		if (command.EndDate != assignment.EndDate && command.EndDate.HasValue)
			assignment.EndDate = command.EndDate.Value;

		await dbContext.SaveChangesAsync(cancellationToken);

		return Result.Success(new UpdateAssignmentResponse(assignment));
	}
}