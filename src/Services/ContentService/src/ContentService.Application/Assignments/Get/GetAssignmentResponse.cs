using ContentService.Domain.Assignments;

namespace ContentService.Application.Assignments.Get;

public class GetAssignmentResponse(Assignment assignment) : AssignmentResponse(assignment);