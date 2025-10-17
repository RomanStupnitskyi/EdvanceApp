using ContentService.Application.Messaging;

namespace ContentService.Application.Courses.Update;

public sealed record UpdateCourseCommand(Guid CourseId) : ICommand<UpdateCourseResponse>
{
	public string? Title { get; set; }

	public string? Description { get; set; }

	public bool? IsVisible { get; set; }
}