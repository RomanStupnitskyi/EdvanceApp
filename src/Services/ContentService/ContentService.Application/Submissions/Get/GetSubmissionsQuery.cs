using ContentService.Application.Messaging;

namespace ContentService.Application.Submissions.Get;

public record GetSubmissionsQuery : IQuery<List<GetSubmissionResponse>>;