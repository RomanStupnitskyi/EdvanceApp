using ContentService.Application.Messaging;

namespace ContentService.Application.Courses.Delete;

public sealed record DeleteCourseCommand(Guid CourseId) : ICommand;