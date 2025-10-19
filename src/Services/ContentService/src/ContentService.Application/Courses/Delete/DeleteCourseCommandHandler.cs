using ContentService.Domain.Courses;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace ContentService.Application.Courses.Delete;

internal sealed class DeleteCourseCommandHandler(
	HybridCache cache,
	IApplicationDbContext dbContext)
	: ICommandHandler<DeleteCourseCommand>
{
	public async Task<Result> Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
	{
		Course? course = await dbContext.Courses
			.SingleOrDefaultAsync(c => c.Id == command.CourseId, cancellationToken);

		if (course is null)
			return Result.Failure(CourseErrors.NotFound(command.CourseId));

		dbContext.Courses.Remove(course);
		await dbContext.SaveChangesAsync(cancellationToken);
		
		string cacheKey = $"course:{command.CourseId}";
		await cache.RemoveAsync(cacheKey, cancellationToken: cancellationToken);

		return Result.Success();
	}
}
