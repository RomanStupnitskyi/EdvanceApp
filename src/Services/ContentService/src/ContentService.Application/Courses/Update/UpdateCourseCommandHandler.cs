using ContentService.Domain.Courses;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace ContentService.Application.Courses.Update;

internal sealed class UpdateCourseCommandHandler(
	HybridCache cache,
	IApplicationDbContext dbContext)
	: ICommandHandler<UpdateCourseCommand, UpdateCourseResponse>
{
	public async Task<Result<UpdateCourseResponse>> Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
	{
		Course? course = await dbContext.Courses
			.SingleOrDefaultAsync(c => c.Id == command.CourseId, cancellationToken);

		if (course is null)
			return Result.Failure<UpdateCourseResponse>(CourseErrors.NotFound(command.CourseId));

		if (command.Title != course.Title && command.Title != null)
			course.Title = command.Title.Trim();
		
		if (command.Description != course.Description && command.Description != null)
			course.Description = command.Description.Trim();
		
		if (command.IsVisible.HasValue && command.IsVisible.Value != course.IsVisible)
			course.IsVisible = command.IsVisible.Value;
		
		course.LastModifiedAt = DateTime.UtcNow;
		course.LastModifiedBy = Guid.Empty;

		await dbContext.SaveChangesAsync(cancellationToken);
		
		string cacheKey = $"course:{course.Id}";
		await cache.SetAsync(cacheKey, course, cancellationToken: cancellationToken);

		return Result.Success(new UpdateCourseResponse(course));
	}
}
