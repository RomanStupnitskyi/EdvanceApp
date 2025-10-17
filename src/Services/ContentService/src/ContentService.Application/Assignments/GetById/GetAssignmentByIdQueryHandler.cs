using ContentService.Domain.Assignments;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace ContentService.Application.Assignments.GetById;

public class GetAssignmentByIdQueryHandler(
	HybridCache cache,
	IApplicationDbContext dbContext)
	: IQueryHandler<GetAssignmentByIdQuery, AssignmentByIdResponse>
{
	public async Task<Result<AssignmentByIdResponse>> Handle(GetAssignmentByIdQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);
		
		var cacheKey = $"assignment:{query.AssignmentId}";
		var assignment = await cache.GetOrCreateAsync(cacheKey, async entry =>
		{
			return await dbContext.Assignments
				.AsNoTracking()
				.FirstOrDefaultAsync(a => a.Id == query.AssignmentId, entry).ConfigureAwait(false);
		}, cancellationToken: cancellationToken).ConfigureAwait(false);

		return assignment is null
			? Result.Failure<AssignmentByIdResponse>(AssignmentErrors.NotFound(query.AssignmentId))
			: Result.Success(new AssignmentByIdResponse(assignment));
	}
}