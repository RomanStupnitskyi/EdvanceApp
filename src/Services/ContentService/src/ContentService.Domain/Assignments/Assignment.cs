using ContentService.SharedKernel;

namespace ContentService.Domain.Assignments;

public class Assignment : Entity
{
	public Guid Id { get; init; }
	public Guid CourseId { get; init; }
	public required string Title { get; set; }
	public string? Description { get; set; }
	public bool AllowLateSubmissions { get; set; }
	public bool AllowResubmissions { get; set; }
	public bool IsVisible { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public DateTime CreatedAt { get; init; }
	public Guid CreatedBy { get; set; }
	public Guid? LastModifiedBy { get; set; }
	public DateTime? LastModifiedAt { get; set; }
}