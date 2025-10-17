using FluentValidation;

namespace ContentService.Application.Assignments.Create;

public class CreateAssignmentValidator : AbstractValidator<CreateAssignmentCommand>
{
	public CreateAssignmentValidator()
	{
		RuleFor(command => command.CourseId)
			.NotEmpty()
			.WithMessage("Course ID is required.");

		RuleFor(command => command.Title)
			.NotEmpty()
			.WithMessage("Title is required.")
			.MaximumLength(200)
			.WithMessage("Title must not exceed 200 characters.");

		RuleFor(command => command.Description)
			.MaximumLength(1000)
			.WithMessage("Description must not exceed 1000 characters.");

		RuleFor(command => command.AllowLateSubmissions)
			.NotNull()
			.WithMessage("AllowLateSubmissions must be specified.");

		RuleFor(command => command.AllowResubmissions)
			.NotNull()
			.WithMessage("AllowResubmissions must be specified.");

		RuleFor(command => command.IsVisible)
			.NotNull()
			.WithMessage("IsVisible must be specified.");

		RuleFor(command => command.StartDate)
			.GreaterThanOrEqualTo(DateTime.UtcNow)
			.When(x => x.StartDate.HasValue)
			.WithMessage("Start date must be in the future.");

		RuleFor(command => command.EndDate)
			.GreaterThanOrEqualTo(x => x.StartDate ?? DateTime.UtcNow)
			.When(x => x.EndDate.HasValue)
			.WithMessage("End date must be after the start date.");
	}
}