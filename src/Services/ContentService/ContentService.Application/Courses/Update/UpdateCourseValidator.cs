using FluentValidation;

namespace ContentService.Application.Courses.Update;

public class UpdateCourseValidator : AbstractValidator<UpdateCourseCommand>
{
	public UpdateCourseValidator()
	{
		RuleFor(command => command.Title)
			.MaximumLength(100)
			.WithMessage("Title must not exceed 100 characters.");
		
		RuleFor(command => command.Description)
			.MaximumLength(500)
			.WithMessage("Description must not exceed 500 characters.");
	}
}