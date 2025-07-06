using ContentService.Domain.AssignmentSubmissions;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Assignments.GetById;
using ContentService.Application.Messaging;

namespace ContentService.Application.Submissions.SubmitAssignment;

public class SubmitAssignmentCommandHandler(
	IQueryHandler<GetAssignmentByIdQuery, AssignmentResponse> getAssignmentHandler,
	IApplicationDbContext dbContext)
	: ICommandHandler<SubmitAssignmentCommand, AssignmentSubmittedResponse>
{
	public async Task<Result<AssignmentSubmittedResponse>> Handle(
		SubmitAssignmentCommand command,
		CancellationToken cancellationToken)
	{
		var query = new GetAssignmentByIdQuery(command.AssignmentId);

		var getAssignmentResult = await getAssignmentHandler.Handle(query, cancellationToken);

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
		
		await dbContext.AssignmentSubmissions.AddAsync(assignmentSubmission, cancellationToken);
		
		await dbContext.SaveChangesAsync(cancellationToken);

		return Result.Success(new AssignmentSubmittedResponse(assignmentSubmission));
	}
}