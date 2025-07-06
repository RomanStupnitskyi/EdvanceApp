using ContentService.Application.Messaging;

namespace ContentService.Application.Submissions.GetByAssignmentId;

public record GetSubmissionsByAssignmentIdQuery(Guid AssignmentId) : IQuery<List<AssignmentSubmissionResponse>>;