using System.Diagnostics.CodeAnalysis;

namespace ContentService.SharedKernel;

public record ApiError(string Code, string Description, ErrorType Type)
{
	public static readonly ApiError None = new(string.Empty, string.Empty, ErrorType.Failure);
	public static readonly ApiError NullValue = new(
		"General.Null",
		"Null value was provided",
		ErrorType.Failure);

	public static ApiError Failure(string code, string description) =>
		new(code, description, ErrorType.Failure);

	public static ApiError NotFound(string code, string description) =>
		new(code, description, ErrorType.NotFound);

	public static ApiError Problem(string code, string description) =>
		new(code, description, ErrorType.Problem);

	public static ApiError Conflict(string code, string description) =>
		new(code, description, ErrorType.Conflict);
}