using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace ContentService.Application.Assignments.DeleteByCourseId;

public class DeleteAssignmentsByCourseIdCommandHandler(
	HybridCache cache,
	IApplicationDbContext dbContext)
	: ICommandHandler<DeleteAssignmentsByCourseIdCommand, DeletedAssignmentsResponse>
{
	public async Task<Result<DeletedAssignmentsResponse>> Handle(
		DeleteAssignmentsByCourseIdCommand command,
		CancellationToken cancellationToken)
	{
		var assignments = await dbContext.Assignments
			.Where(a => a.CourseId == command.CourseId)
			.ToListAsync(cancellationToken);

		if (assignments.Count == 0)
			return Result.Success(
				new DeletedAssignmentsResponse { DeletedAssignmentsCount = 0 });
		
		dbContext.Assignments.RemoveRange(assignments);
		var deletedCount = await dbContext.SaveChangesAsync(cancellationToken);
		
		foreach (var cacheKey in assignments.Select(assignment => $"assignment:{assignment.Id}"))
			await cache.RemoveAsync(cacheKey, cancellationToken);
		
		return Result.Success(new DeletedAssignmentsResponse { DeletedAssignmentsCount = deletedCount });
	}
}