using ContentService.SharedKernel;

namespace ContentService.Domain.AssignmentSubmissions;

public class AssignmentSubmissionErrors
{
	public static Error NotFound(Guid submissionId) => Error.NotFound(
		"AssignmentSubmission.NotFound",
		$"The submission with the Id = '{submissionId}' was not found");
	
	public static Error AssignmentNotFound(Guid assignmentId) => Error.NotFound(
		"AssignmentSubmission.AssignmentNotFound",
		$"The assignment with the Id = '{assignmentId}' was not found");
}