using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Application.Courses.Get;

public class GetCourseQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetCoursesQuery, List<GetCourseResponse>>
{
	public async Task<Result<List<GetCourseResponse>>> Handle(
		GetCoursesQuery query,
		CancellationToken cancellationToken)
	{
		var courses = await dbContext.Courses
			.Select(course => new GetCourseResponse(course))
			.ToListAsync(cancellationToken);
		
		return Result.Success(courses);
	}
}