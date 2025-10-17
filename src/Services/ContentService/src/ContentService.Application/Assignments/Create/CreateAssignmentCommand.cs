using ContentService.Application.Messaging;

namespace ContentService.Application.Assignments.Create;

public sealed record CreateAssignmentCommand : ICommand<CreateAssignmentResponse>
{
	public Guid CourseId { get; set; }
	public required string Title { get; set; }
	public string? Description { get; set; }
	public bool AllowLateSubmissions { get; set; }
	public bool AllowResubmissions { get; set; }
	public bool IsVisible { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
}