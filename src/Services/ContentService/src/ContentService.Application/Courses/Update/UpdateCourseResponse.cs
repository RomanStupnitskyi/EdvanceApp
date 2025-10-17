using ContentService.Domain.Courses;

namespace ContentService.Application.Courses.Update;

public sealed class UpdateCourseResponse(Course course) : CourseResponse(course);