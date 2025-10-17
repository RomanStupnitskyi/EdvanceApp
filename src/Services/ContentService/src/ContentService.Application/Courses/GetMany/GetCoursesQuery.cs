using ContentService.Application.Messaging;

namespace ContentService.Application.Courses.GetMany;

public sealed record GetCoursesQuery : IQuery<List<GetCourseResponse>>;