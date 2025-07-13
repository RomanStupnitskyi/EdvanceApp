using ContentService.Domain.Assignments;

namespace ContentService.Application.Assignments.GetById;

public class AssignmentByIdResponse(Assignment assignment) : AssignmentResponse(assignment);