using ContentService.Domain.AssignmentSubmissions;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace ContentService.Application.Submissions.GetById;

public class GetSubmissionByIdQueryHandler(
	HybridCache cache,
	IApplicationDbContext dbContext)
	: IQueryHandler<GetSubmissionByIdQuery, GetSubmissionByIdResponse>
{
	public async Task<Result<GetSubmissionByIdResponse>> Handle(
		GetSubmissionByIdQuery query,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);
		
		var cacheKey = $"submission:{query.SubmissionId}";
		var submission = await cache.GetOrCreateAsync(cacheKey, async entry =>
		{
			return await dbContext.AssignmentSubmissions
				.Where(submission => submission.Id == query.SubmissionId)
				.Select(submission => new GetSubmissionByIdResponse(submission))
				.SingleOrDefaultAsync(entry)
				.ConfigureAwait(false);
		}, cancellationToken: cancellationToken).ConfigureAwait(false);
		
		if (submission is null)
			return Result.Failure<GetSubmissionByIdResponse>(
				AssignmentSubmissionErrors.NotFound(query.SubmissionId));

		return Result.Success(submission);
	}
}