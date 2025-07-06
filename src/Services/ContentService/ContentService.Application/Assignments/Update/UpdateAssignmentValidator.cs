using FluentValidation;

namespace ContentService.Application.Assignments.Update;

public class UpdateAssignmentValidator : AbstractValidator<UpdateAssignmentCommand>
{
	public UpdateAssignmentValidator()
	{
		RuleFor(x => x.AssignmentId)
			.NotEmpty()
			.WithMessage("Assignment ID is required.");

		RuleFor(x => x.Title)
			.MaximumLength(100)
			.WithMessage("Title must not exceed 100 characters.");

		RuleFor(x => x.Description)
			.MaximumLength(500)
			.WithMessage("Description must not exceed 500 characters.");

		RuleFor(x => x.StartDate)
			.GreaterThanOrEqualTo(DateTime.UtcNow)
			.When(x => x.StartDate.HasValue)
			.WithMessage("Start date must be in the future.");

		RuleFor(x => x.EndDate)
			.GreaterThanOrEqualTo(x => x.StartDate)
			.When(x => x.EndDate.HasValue && x.StartDate.HasValue)
			.WithMessage("End date must be after the start date.");
	}
}