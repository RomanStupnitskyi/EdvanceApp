using ContentService.Domain.AssignmentSubmissions;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Application.Submissions.GetById;

public class GetSubmissionByIdQueryHandler(
	IApplicationDbContext dbContext)
	: IQueryHandler<GetSubmissionByIdQuery, GetSubmissionByIdResponse>
{
	public async Task<Result<GetSubmissionByIdResponse>> Handle(
		GetSubmissionByIdQuery query,
		CancellationToken cancellationToken)
	{
		var submission = await dbContext.AssignmentSubmissions
			.Where(submission => submission.Id == query.SubmissionId)
			.Select(submission => new GetSubmissionByIdResponse(submission))
			.SingleOrDefaultAsync(cancellationToken);
		
		if (submission is null)
			return Result.Failure<GetSubmissionByIdResponse>(
				AssignmentSubmissionErrors.NotFound(query.SubmissionId));

		return Result.Success(submission);
	}
}