using FluentValidation;

namespace ContentService.Application.Submissions.SubmitAssignment;

public class SubmitAssignmentValidator : AbstractValidator<SubmitAssignmentCommand>
{
	public SubmitAssignmentValidator()
	{
		RuleFor(command => command.AssignmentId)
			.NotEmpty()
			.WithMessage("Assignment ID is required");
		
		RuleFor(command => command.StudentId)
			.NotEmpty()
			.WithMessage("Student ID is required");
		
		RuleFor(command => command.Content)
			.NotEmpty()
			.WithMessage("Content is required");
	}
}