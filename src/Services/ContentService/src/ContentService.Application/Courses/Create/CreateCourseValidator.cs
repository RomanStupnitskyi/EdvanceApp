using FluentValidation;

namespace ContentService.Application.Courses.Create;

public class CreateCourseValidator : AbstractValidator<CreateCourseCommand>
{
	public CreateCourseValidator()
	{
		RuleFor(command => command.Title)
			.NotEmpty()
			.WithMessage("Title is required.")
            .WithErrorCode("TitleRequired")
			.MaximumLength(100)
			.WithMessage("Title must not exceed 100 characters.")
            .WithErrorCode("TitleMaxLengthExceeded");
		
		RuleFor(command => command.Description)
			.NotEmpty()
			.WithMessage("Description is required.")
            .WithErrorCode("DescriptionRequired")
			.MaximumLength(500)
			.WithMessage("Description must not exceed 500 characters.")
            .WithErrorCode("DescriptionMaxLengthExceeded");
	}
}
