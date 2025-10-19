using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Application.Assignments.GetMany;

internal sealed class GetAssignmentQueryHandler(IApplicationDbContext dbContext)
	: IQueryHandler<GetAssignmentQuery, List<GetAssignmentResponse>>
{
	public async Task<Result<List<GetAssignmentResponse>>> Handle(GetAssignmentQuery query, CancellationToken cancellationToken)
	{
		List<GetAssignmentResponse> assignments = await dbContext.Assignments
			.Select(assignment => new GetAssignmentResponse(assignment))
			.ToListAsync(cancellationToken);

		return Result.Success(assignments);
	}
}
