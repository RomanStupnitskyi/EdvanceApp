using ContentService.Domain.Courses;

namespace ContentService.Application.Courses.Get;

public sealed class GetCourseResponse(Course course) : CourseResponse(course);