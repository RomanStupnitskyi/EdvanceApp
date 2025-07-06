using ContentService.Application.Messaging;

namespace ContentService.Application.Submissions.GetById;

public record GetSubmissionByIdQuery(Guid SubmissionId) : IQuery<GetSubmissionByIdResponse>;