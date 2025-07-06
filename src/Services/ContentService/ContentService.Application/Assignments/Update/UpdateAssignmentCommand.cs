using ContentService.Application.Messaging;

namespace ContentService.Application.Assignments.Update;

public sealed record UpdateAssignmentCommand : ICommand<UpdateAssignmentResponse>
{
	public Guid AssignmentId { get; init; }
	public string? Title { get; set; }
	public string? Description { get; set; }
	public bool? AllowLateSubmissions { get; set; }
	public bool? AllowResubmissions { get; set; }
	public bool? IsVisible { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
};