using ContentService.Domain.Assignments;

namespace ContentService.Application.Assignments.GetMany;

public class GetAssignmentResponse(Assignment assignment) : AssignmentResponse(assignment);