using ContentService.Domain.Courses;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Application.Courses.GetById;

public class GetCourseByIdQueryHandler(IApplicationDbContext dbContext)
	: IQueryHandler<GetCourseByIdQuery, CourseResponse>
{
	public async Task<Result<CourseResponse>> Handle(GetCourseByIdQuery query, CancellationToken cancellationToken)
	{
		var course = await dbContext.Courses
			.Where(course => course.Id == query.CourseId)
			.Select(course => new CourseResponse(course))
			.SingleOrDefaultAsync(cancellationToken);

		return course is null
			? Result.Failure<CourseResponse>(CourseErrors.NotFound(query.CourseId))
			: Result.Success(course);
	}
}