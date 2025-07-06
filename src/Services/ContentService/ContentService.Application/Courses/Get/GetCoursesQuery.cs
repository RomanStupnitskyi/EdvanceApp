using ContentService.Application.Messaging;

namespace ContentService.Application.Courses.Get;

public sealed record GetCoursesQuery : IQuery<List<GetCourseResponse>>;