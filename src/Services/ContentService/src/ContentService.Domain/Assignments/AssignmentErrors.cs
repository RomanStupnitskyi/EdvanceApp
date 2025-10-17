using ContentService.SharedKernel;

namespace ContentService.Domain.Assignments;

public static class AssignmentErrors
{
	public static ApiError NotFound(Guid assignmentId) => ApiError.NotFound(
		"Assignment.NotFound",
		$"The assignment with the Id = '{assignmentId}' was not found");
	
	public static ApiError CourseNotFound(Guid courseId) => ApiError.NotFound(
		"Assignment.CourseNotFound",
		$"The course with the Id = '{courseId}' was not found");
}