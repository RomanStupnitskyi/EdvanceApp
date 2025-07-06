using ContentService.Application.Messaging;

namespace ContentService.Application.Assignments.GetByCourseId;

public sealed record GetAssignmentByCourseIdQuery(Guid CourseId) : IQuery<List<CourseAssignmentResponse>>;