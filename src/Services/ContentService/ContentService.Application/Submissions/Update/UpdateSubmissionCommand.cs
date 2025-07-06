using ContentService.Application.Messaging;

namespace ContentService.Application.Submissions.Update;

public record UpdateSubmissionCommand(Guid SubmissionId) : ICommand<UpdateSubmissionResponse>
{
	public string? Content { get; set; }
}