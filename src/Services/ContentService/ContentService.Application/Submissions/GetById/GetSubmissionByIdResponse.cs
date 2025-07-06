using ContentService.Domain.AssignmentSubmissions;

namespace ContentService.Application.Submissions.GetById;

public class GetSubmissionByIdResponse(AssignmentSubmission submission)
{
	public Guid Id { get; set; } = submission.Id;
	
	public Guid AssignmentId { get; set; } = submission.AssignmentId;
	
	public Guid StudentId { get; set; } = submission.StudentId;
	
	public string Content { get; set; } =  submission.Content;
	
	public DateTime SubmittedAt { get; set; } = submission.SubmittedAt;
	
	public DateTime? LastUpdatedAt { get; set; } = submission.LastUpdatedAt;
}