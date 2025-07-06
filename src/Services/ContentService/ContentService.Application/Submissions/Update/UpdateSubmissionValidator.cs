using FluentValidation;

namespace ContentService.Application.Submissions.Update;

public class UpdateSubmissionValidator : AbstractValidator<UpdateSubmissionCommand>
{
	public UpdateSubmissionValidator()
	{
		RuleFor(command => command.SubmissionId)
			.NotEmpty()
			.WithMessage("Submission ID is required");
		
		RuleFor(command => command.Content)
			.NotEmpty()
			.WithMessage("Content is required");
	}
}