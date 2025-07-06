namespace ContentService.Domain.Assignments.DTOs;

public class CreateAssignmentDto
{
	public string Title { get; set; }
	public string? Description { get; set; }
	public bool AllowLateSubmissions { get; set; }
	public bool AllowResubmissions { get; set; }
	public bool IsVisible { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
}