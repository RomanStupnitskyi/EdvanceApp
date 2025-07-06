using ContentService.SharedKernel;

namespace ContentService.Domain.Assignments;

public class AssignmentsDeletedDomainEvent : IDomainEvent
{
	Guid[] AssignmentIds { get; set; }
}