using ContentService.Domain.Assignments;

namespace ContentService.Application.Assignments.GetByCourseId;

public class CourseAssignmentResponse(Assignment assignment) : AssignmentResponse(assignment);