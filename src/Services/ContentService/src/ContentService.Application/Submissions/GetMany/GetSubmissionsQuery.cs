using ContentService.Application.Messaging;

namespace ContentService.Application.Submissions.GetMany;

public record GetSubmissionsQuery : IQuery<List<GetSubmissionResponse>>;