using ContentService.SharedKernel;

namespace ContentService.Domain.AssignmentSubmissions;

public static class AssignmentSubmissionErrors
{
	public static ApiError NotFound(Guid submissionId) => ApiError.NotFound(
		"AssignmentSubmission.NotFound",
		$"The submission with the Id = '{submissionId}' was not found");
	
	public static ApiError AssignmentNotFound(Guid assignmentId) => ApiError.NotFound(
		"AssignmentSubmission.AssignmentNotFound",
		$"The assignment with the Id = '{assignmentId}' was not found");
}