using ContentService.Application.Messaging;

namespace ContentService.Application.Courses.GetById;

public sealed record GetCourseByIdQuery(Guid CourseId) : IQuery<CourseResponse>;