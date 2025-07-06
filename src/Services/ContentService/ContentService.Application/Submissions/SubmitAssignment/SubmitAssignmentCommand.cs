using ContentService.Application.Messaging;

namespace ContentService.Application.Submissions.SubmitAssignment;

public sealed record SubmitAssignmentCommand : ICommand<AssignmentSubmittedResponse>
{
	public Guid AssignmentId { get; init; }
	
	public Guid StudentId { get; init; }
	
	public string Content { get; set; }
}