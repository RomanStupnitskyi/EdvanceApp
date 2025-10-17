using ContentService.Application.Messaging;

namespace ContentService.Application.Assignments.GetMany;

public sealed record GetAssignmentQuery : IQuery<List<GetAssignmentResponse>>;