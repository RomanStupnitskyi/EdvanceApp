using ContentService.Application.Messaging;

namespace ContentService.Application.Assignments.Delete;

public sealed record DeleteAssignmentCommand(Guid AssignmentId) : ICommand;