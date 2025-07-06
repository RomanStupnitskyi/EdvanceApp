using ContentService.Application.Messaging;

namespace ContentService.Application.Assignments.DeleteByCourseId;

public sealed record DeleteAssignmentsByCourseIdCommand : ICommand<DeletedAssignmentsResponse>
{
	public Guid CourseId { get; set; }
}