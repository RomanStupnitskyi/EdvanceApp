using System.Diagnostics.CodeAnalysis;

namespace ContentService.SharedKernel;

public class Result
{
	public Result(bool isSuccess, ApiError error)
	{
		if (isSuccess && error != ApiError.None ||
		    !isSuccess && error == ApiError.None)
		{
			throw new ArgumentException("Invalid error", nameof(error));
		}

		IsSuccess = isSuccess;
		Error = error;
	}

	public bool IsSuccess { get; }

	public bool IsFailure => !IsSuccess;

	public ApiError Error { get; }

	public static Result Success() => new(true, ApiError.None);

	public static Result<TValue> Success<TValue>(TValue value) =>
		new(value, true, ApiError.None);

	public static Result Failure(ApiError error) => new(false, error);

	public static Result<TValue> Failure<TValue>(ApiError error) =>
		new(default, false, error);
}

[SuppressMessage("ReSharper", "NullableWarningSuppressionIsUsed")]
public class Result<TValue>(TValue? value, bool isSuccess, ApiError error) : Result(isSuccess, error)
{
	[NotNull]
	public TValue Value => IsSuccess
		? value!
		: throw new InvalidOperationException("The value of a failure result can't be accessed.");

	public static implicit operator Result<TValue>(TValue? value) =>
		value is not null ? Success(value) : Failure<TValue>(ApiError.NullValue);

	[SuppressMessage("Design", "CA1000:Do not declare static members on generic types")]
	public static Result<TValue> ValidationFailure(ApiError error) =>
		new(default, false, error);

    public Result<TValue> ToResult()
		=> value is not null ? Success(value) : Failure<TValue>(ApiError.NullValue);
}
