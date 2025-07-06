using ContentService.Application.Messaging;

namespace ContentService.Application.Assignments.GetById;

public sealed record GetAssignmentByIdQuery(Guid AssignmentId) : IQuery<AssignmentResponse>;