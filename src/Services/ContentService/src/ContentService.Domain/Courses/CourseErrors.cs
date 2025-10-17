using ContentService.SharedKernel;

namespace ContentService.Domain.Courses;

public static class CourseErrors
{
	public static Error NotFound(Guid courseId) => Error.NotFound(
		"Course.NotFound",
		$"The course with the Id = '{courseId}' was not found");
}