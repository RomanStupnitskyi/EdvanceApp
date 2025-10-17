using ContentService.Domain.Courses;

namespace ContentService.Application.Courses.GetMany;

public sealed class GetCourseResponse(Course course) : CourseResponse(course);