using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Application.Assignments.DeleteByCourseId;

public class DeleteAssignmentsByCourseIdCommandHandler(IApplicationDbContext dbContext)
	: ICommandHandler<DeleteAssignmentsByCourseIdCommand, DeletedAssignmentsResponse>
{
	public async Task<Result<DeletedAssignmentsResponse>> Handle(DeleteAssignmentsByCourseIdCommand command, CancellationToken cancellationToken)
	{
		var assignments = await dbContext.Assignments
			.Where(a => a.CourseId == command.CourseId)
			.ToListAsync(cancellationToken);

		if (assignments.Count == 0)
			return Result.Success(
				new DeletedAssignmentsResponse { DeletedAssignmentsCount = 0 });
		
		dbContext.Assignments.RemoveRange(assignments);
		var deletedCount = await dbContext.SaveChangesAsync(cancellationToken);
		
		return Result.Success(new DeletedAssignmentsResponse { DeletedAssignmentsCount = deletedCount });
	}
}