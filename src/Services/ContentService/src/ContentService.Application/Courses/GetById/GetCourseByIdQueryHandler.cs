using ContentService.Domain.Courses;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace ContentService.Application.Courses.GetById;

public class GetCourseByIdQueryHandler(
	IApplicationDbContext dbContext,
	HybridCache cache)
	: IQueryHandler<GetCourseByIdQuery, CourseByIdResponse>
{
	public async Task<Result<CourseByIdResponse>> Handle(GetCourseByIdQuery query, CancellationToken cancellationToken)
	{
		var cacheKey = $"course:{query.CourseId}";

		var course = await cache.GetOrCreateAsync(cacheKey, async entry =>
		{
			return await dbContext.Courses
				.Where(course => course.Id == query.CourseId)
				.SingleOrDefaultAsync(entry);
		}, cancellationToken: cancellationToken);

		return course is null
			? Result.Failure<CourseByIdResponse>(CourseErrors.NotFound(query.CourseId))
			: Result.Success(new CourseByIdResponse(course));
	}
}