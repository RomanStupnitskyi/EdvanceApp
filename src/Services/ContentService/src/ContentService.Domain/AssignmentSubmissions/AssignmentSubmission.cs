using ContentService.SharedKernel;

namespace ContentService.Domain.AssignmentSubmissions;

public class AssignmentSubmission : Entity
{
	public Guid Id { get; init; } = Guid.NewGuid();
	
	public Guid AssignmentId { get; init; }
	
	public Guid StudentId { get; init; }
	
	public required string Content { get; set; }
	
	public DateTime SubmittedAt { get; init; } = DateTime.UtcNow;
	
	public DateTime? LastUpdatedAt { get; set; }
}