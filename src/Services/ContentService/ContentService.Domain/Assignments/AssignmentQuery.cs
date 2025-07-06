using ContentService.SharedKernel;

namespace ContentService.Domain.Assignments;

public class AssignmentQuery : BaseQuery
{
	public Guid[]? CourseIds { get; set; }
	public bool? Active { get; set; }
}