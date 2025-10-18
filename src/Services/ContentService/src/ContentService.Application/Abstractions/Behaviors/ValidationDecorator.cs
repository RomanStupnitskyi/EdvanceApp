using ContentService.SharedKernel;
using ContentService.Application.Messaging;
using FluentValidation;
using FluentValidation.Results;

namespace ContentService.Application.Abstractions.Behaviors;

public static class ValidationDecorator
{
	internal sealed class CommandHandler<TCommand, TResponse>(
		ICommandHandler<TCommand, TResponse> innerHandler,
		IEnumerable<IValidator<TCommand>> validators)
		: ICommandHandler<TCommand, TResponse>
		where TCommand : ICommand<TResponse>
	{
		public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
		{
			ValidationFailure[] validationFailures = await ValidateAsync(command, validators).ConfigureAwait(false);
			
			if (validationFailures.Length == 0)
				return await innerHandler.Handle(command, cancellationToken).ConfigureAwait(false);
			
			return Result.Failure<TResponse>(CreateValidationError(validationFailures));
		}
	}

	internal sealed class CommandBaseHandler<TCommand>(
		ICommandHandler<TCommand> innerHandler,
		IEnumerable<IValidator<TCommand>> validators)
		: ICommandHandler<TCommand>
		where TCommand : ICommand
	{
		public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
		{
			ValidationFailure[] validationFailures = await ValidateAsync(command, validators).ConfigureAwait(false);
			
			if (validationFailures.Length == 0)
				return await innerHandler.Handle(command, cancellationToken).ConfigureAwait(false);
			
			return Result.Failure(CreateValidationError(validationFailures));
		}
	}
	
	private static async Task<ValidationFailure[]> ValidateAsync<TCommand>(
		TCommand command,
		IEnumerable<IValidator<TCommand>> validators)
	{
		IValidator<TCommand>[] enumerable = [.. validators];
		if (enumerable.Length == 0)
			return [];
		
		var context = new ValidationContext<TCommand>(command);
		
		ValidationResult[] validationResults = await Task.WhenAll(
			enumerable.Select(v => v.ValidateAsync(context))).ConfigureAwait(false);

		ValidationFailure[] failures = [.. validationResults
			.Where(result => !result.IsValid)
			.SelectMany(result => result.Errors)];

		return failures;
	}
	
	private static ValidationError CreateValidationError(ValidationFailure[] validationFailures) =>
		new([.. validationFailures.Select(failure => ApiError.Problem(failure.ErrorCode, failure.ErrorMessage))]);
}