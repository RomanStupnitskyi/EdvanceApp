using ContentService.Domain.Assignments;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace ContentService.Application.Assignments.Delete;

public class DeleteAssignmentCommandHandler(
	HybridCache cache,
	IApplicationDbContext dbContext)
	: ICommandHandler<DeleteAssignmentCommand>
{
	public async Task<Result> Handle(DeleteAssignmentCommand command, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(command);
		
		var assignment = await dbContext.Assignments
			.SingleOrDefaultAsync(assignment => assignment.Id == command.AssignmentId, cancellationToken).ConfigureAwait(false);

		if (assignment is null)
			return Result.Failure(AssignmentErrors.NotFound(command.AssignmentId));

		dbContext.Assignments.Remove(assignment);
		await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		
		var cacheKey = $"assignment:{assignment.Id}";
		await cache.RemoveAsync(cacheKey, cancellationToken).ConfigureAwait(false);

		return Result.Success();
	}
}