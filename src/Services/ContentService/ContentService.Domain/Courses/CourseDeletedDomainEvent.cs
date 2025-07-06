using ContentService.SharedKernel;

namespace ContentService.Domain.Courses;

public class CourseDeletedDomainEvent : IDomainEvent
{
	public Guid CourseId { get; set; }
}