using ContentService.SharedKernel;

namespace ContentService.Domain.Courses;

public static class CourseErrors
{
	public static ApiError NotFound(Guid courseId) => ApiError.NotFound(
		"Course.NotFound",
		$"The course with the Id = '{courseId}' was not found");
}