namespace ContentService.Domain.Courses.DTOs;

public sealed class CreateCourseDto
{
	public string Title { get; set; }
	public string? Description { get; set; }
	public bool IsVisible { get; set; }
}