using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Application.Assignments.GetMany;

public class GetAssignmentQueryHandler(IApplicationDbContext dbContext)
	: IQueryHandler<GetAssignmentQuery, List<GetAssignmentResponse>>
{
	public async Task<Result<List<GetAssignmentResponse>>> Handle(GetAssignmentQuery query, CancellationToken cancellationToken)
	{
		var assignments = await dbContext.Assignments
			.Select(assignment => new GetAssignmentResponse(assignment))
			.ToListAsync(cancellationToken).ConfigureAwait(false);

		return Result.Success(assignments);
	}
}