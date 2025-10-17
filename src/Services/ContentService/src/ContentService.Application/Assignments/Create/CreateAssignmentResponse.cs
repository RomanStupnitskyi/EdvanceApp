using ContentService.Domain.Assignments;

namespace ContentService.Application.Assignments.Create;

public class CreateAssignmentResponse(Assignment assignment) : AssignmentResponse(assignment);