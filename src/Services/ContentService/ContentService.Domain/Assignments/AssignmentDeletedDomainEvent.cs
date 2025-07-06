using ContentService.SharedKernel;

namespace ContentService.Domain.Assignments;

public class AssignmentDeletedDomainEvent : IDomainEvent
{
	Guid AssignmentId { get; set; }
}