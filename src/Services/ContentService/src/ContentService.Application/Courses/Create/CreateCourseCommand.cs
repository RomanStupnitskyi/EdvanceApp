using ContentService.Application.Messaging;

namespace ContentService.Application.Courses.Create;

public sealed record CreateCourseCommand : ICommand<CreateCourseResponse>
{
	public required string Title { get; set; }
	public string? Description { get; set; }
	public bool IsVisible { get; set; } = true;
	public Guid CreatedBy { get; set; }
}