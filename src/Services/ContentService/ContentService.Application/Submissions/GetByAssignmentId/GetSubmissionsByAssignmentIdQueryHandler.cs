using ContentService.Domain.AssignmentSubmissions;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Assignments.GetById;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Application.Submissions.GetByAssignmentId;

public class GetSubmissionsByAssignmentIdQueryHandler(
	IQueryHandler<GetAssignmentByIdQuery, AssignmentResponse> handler,
	IApplicationDbContext dbContext)
	: IQueryHandler<GetSubmissionsByAssignmentIdQuery, List<AssignmentSubmissionResponse>>
{
	public async Task<Result<List<AssignmentSubmissionResponse>>> Handle(
		GetSubmissionsByAssignmentIdQuery query,
		CancellationToken cancellationToken)
	{
		var assignmentQuery = new GetAssignmentByIdQuery(query.AssignmentId);

		var assignment = await handler.Handle(assignmentQuery, cancellationToken);

		if (assignment.IsFailure)
			return Result.Failure<List<AssignmentSubmissionResponse>>(
				AssignmentSubmissionErrors.AssignmentNotFound(query.AssignmentId));
		
		var submissions = await dbContext.AssignmentSubmissions
			.Where(submission => submission.AssignmentId == query.AssignmentId)
			.Select(submission => new AssignmentSubmissionResponse(submission))
			.ToListAsync(cancellationToken);

		return Result.Success(submissions);
	}
}