using ContentService.Domain.Assignments;

namespace ContentService.Application.Assignments.GetById;

public class AssignmentResponse(Assignment assignment)
{
	public Guid Id { get; init; } = assignment.Id;
	public Guid CourseId { get; init; } = assignment.CourseId;
	public string Title { get; set; } = assignment.Title;
	public string? Description { get; set; } = assignment.Description;
	public bool AllowLateSubmissions { get; set; } = assignment.AllowLateSubmissions;
	public bool AllowResubmissions { get; set; } = assignment.AllowResubmissions;
	public bool IsVisible { get; set; } = assignment.IsVisible;
	public DateTime? StartDate { get; set; } = assignment.StartDate;
	public DateTime? EndDate { get; set; } = assignment.EndDate;
	public DateTime CreatedAt { get; init; } = assignment.CreatedAt;
	public Guid CreatedBy { get; set; } = assignment.CreatedBy;
	public Guid? LastModifiedBy { get; set; } = assignment.LastModifiedBy;
	public DateTime? LastModifiedAt { get; set; } = assignment.LastModifiedAt;
}