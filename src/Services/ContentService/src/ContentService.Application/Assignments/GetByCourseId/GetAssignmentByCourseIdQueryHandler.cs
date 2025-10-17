using ContentService.Domain.Assignments;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Courses.GetById;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Application.Assignments.GetByCourseId;

public class GetAssignmentByCourseIdQueryHandler(
	IQueryHandler<GetCourseByIdQuery, CourseByIdResponse> handler,
	IApplicationDbContext dbContext)
	: IQueryHandler<GetAssignmentByCourseIdQuery, List<CourseAssignmentResponse>>
{
	public async Task<Result<List<CourseAssignmentResponse>>> Handle(GetAssignmentByCourseIdQuery query, CancellationToken cancellationToken)
	{
		var courseQuery = new GetCourseByIdQuery(query.CourseId);
		
		var course = await handler.Handle(courseQuery, cancellationToken);

		if (course.IsFailure)
			return Result.Failure<List<CourseAssignmentResponse>>(
				AssignmentErrors.CourseNotFound(query.CourseId));
		
		var assignments = await dbContext.Assignments
			.Where(assignment => assignment.CourseId == query.CourseId)
			.Select(assignment => new CourseAssignmentResponse(assignment))
			.ToListAsync(cancellationToken);

		return Result.Success(assignments);
	}
}