using ContentService.Application.Messaging;

namespace ContentService.Application.Assignments.Get;

public sealed record GetAssignmentQuery : IQuery<List<GetAssignmentResponse>>;