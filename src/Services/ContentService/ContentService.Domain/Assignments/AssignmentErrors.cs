using ContentService.SharedKernel;

namespace ContentService.Domain.Assignments;

public class AssignmentErrors
{
	public static Error NotFound(Guid assignmentId) => Error.NotFound(
		"Assignment.NotFound",
		$"The assignment with the Id = '{assignmentId}' was not found");
	
	public static Error CourseNotFound(Guid courseId) => Error.NotFound(
		"Assignment.CourseNotFound",
		$"The course with the Id = '{courseId}' was not found");
}