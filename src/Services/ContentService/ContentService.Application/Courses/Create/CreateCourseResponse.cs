using ContentService.Domain.Courses;

namespace ContentService.Application.Courses.Create;

public class CreateCourseResponse(Course course) : CourseResponse(course);