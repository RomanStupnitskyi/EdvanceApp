using ContentService.Domain.Courses;

namespace ContentService.Application.Courses.GetById;

public sealed class CourseByIdResponse(Course course) : CourseResponse(course);