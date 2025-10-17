using ContentService.SharedKernel;

namespace ContentService.Domain.Courses;

public class Course : Entity
{
	public Guid Id { get; init; }

	public string Title { get; set; }

	public string? Description { get; set; }

	public bool IsVisible { get; set; }
	
	public DateTime CreatedAt { get; init; }
	
	public Guid CreatedBy { get; init; }
	
	public DateTime? LastModifiedAt { get; set; }
	
	public Guid? LastModifiedBy { get; set; }
}