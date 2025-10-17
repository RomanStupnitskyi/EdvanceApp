namespace ContentService.Domain.Courses.DTOs;

public class UpdateCourseDto
{
	public string? Title { get; set; }

	public string? Description { get; set; }

	public bool? IsVisible { get; set; }
}