using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Application.Courses.GetMany;

public class GetCourseQueryHandler(IApplicationDbContext dbContext)
	: IQueryHandler<GetCoursesQuery, List<GetCourseResponse>>
{
	public async Task<Result<List<GetCourseResponse>>> Handle(
		GetCoursesQuery query,
		CancellationToken cancellationToken)
	{
		List<GetCourseResponse> courses = await dbContext.Courses
			.Select(course => new GetCourseResponse(course))
			.ToListAsync(cancellationToken).ConfigureAwait(false);
		
		return Result.Success(courses);
	}
}