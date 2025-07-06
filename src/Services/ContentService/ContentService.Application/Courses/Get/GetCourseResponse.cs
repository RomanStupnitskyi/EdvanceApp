using ContentService.Domain.Courses;

namespace ContentService.Application.Courses.Get;

public sealed class GetCourseResponse(Course course)
{
	public Guid Id { get; init; } = course.Id;

	public string Title { get; set; } = course.Title;

	public string? Description { get; set; } = course.Description;

	public bool IsVisible { get; set; } = course.IsVisible;

	public DateTime CreatedAt { get; init; } = course.CreatedAt;

	public Guid CreatedBy { get; init; } = course.CreatedBy;

	public DateTime? LastModifiedAt { get; set; } = course.LastModifiedAt;

	public Guid? LastModifiedBy { get; set; } = course.LastModifiedBy;
}