using ContentService.Application.Messaging;

namespace ContentService.Application.Submissions.Delete;

public sealed record DeleteSubmissionCommand(Guid SubmissionId) : ICommand;