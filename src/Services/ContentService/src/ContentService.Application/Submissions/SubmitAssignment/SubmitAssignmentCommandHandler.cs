using ContentService.Domain.AssignmentSubmissions;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Assignments.GetById;
using ContentService.Application.Messaging;

namespace ContentService.Application.Submissions.SubmitAssignment;

public class SubmitAssignmentCommandHandler(
	IQueryHandler<GetAssignmentByIdQuery, AssignmentByIdResponse> getAssignmentHandler,
	IApplicationDbContext dbContext)
	: ICommandHandler<SubmitAssignmentCommand, AssignmentSubmittedResponse>
{
	public async Task<Result<AssignmentSubmittedResponse>> Handle(
		SubmitAssignmentCommand command,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(command);
		
		var query = new GetAssignmentByIdQuery(command.AssignmentId);

		var getAssignmentResult = await getAssignmentHandler
			.Handle(query, cancellationToken)
			.ConfigureAwait(false);

		if (getAssignmentResult.IsFailure)
			return Result.Failure<AssignmentSubmittedResponse>(
				AssignmentSubmissionErrors.AssignmentNotFound(command.AssignmentId));
		
		var assignmentSubmission = new AssignmentSubmission
		{
			Id = Guid.NewGuid(),
			AssignmentId = command.AssignmentId,
			StudentId = command.StudentId,
			Content = command.Content,
			SubmittedAt = DateTime.UtcNow
		};
		
		await dbContext.AssignmentSubmissions
			.AddAsync(assignmentSubmission, cancellationToken)
			.ConfigureAwait(false);
		
		await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

		return Result.Success(new AssignmentSubmittedResponse(assignmentSubmission));
	}
}