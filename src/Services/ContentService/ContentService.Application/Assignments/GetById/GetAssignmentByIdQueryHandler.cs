using ContentService.Domain.Assignments;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Application.Assignments.GetById;

public class GetAssignmentByIdQueryHandler(IApplicationDbContext dbContext)
	: IQueryHandler<GetAssignmentByIdQuery, AssignmentResponse>
{
	public async Task<Result<AssignmentResponse>> Handle(GetAssignmentByIdQuery query, CancellationToken cancellationToken)
	{
		var assignment = await dbContext.Assignments
			.AsNoTracking()
			.FirstOrDefaultAsync(a => a.Id == query.AssignmentId, cancellationToken);

		return assignment is null
			? Result.Failure<AssignmentResponse>(AssignmentErrors.NotFound(query.AssignmentId))
			: Result.Success(new AssignmentResponse(assignment));
	}
}