using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Application.Submissions.GetMany;

internal sealed class GetSubmissionsQueryHandler(
	IApplicationDbContext dbContext) : IQueryHandler<GetSubmissionsQuery, List<GetSubmissionResponse>>
{
	public async Task<Result<List<GetSubmissionResponse>>> Handle(
		GetSubmissionsQuery query,
		CancellationToken cancellationToken)
	{
		List<GetSubmissionResponse> submissions = await dbContext.AssignmentSubmissions
			.Select(submission => new GetSubmissionResponse(submission))
			.ToListAsync(cancellationToken: cancellationToken);

		return Result.Success(submissions);
	}
}
