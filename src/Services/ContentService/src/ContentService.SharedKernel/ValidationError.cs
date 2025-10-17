namespace ContentService.SharedKernel;

public sealed record ValidationError(Error[] Errors)
	: Error("Validation.General",
		"One or more validation errors occurred.",
		ErrorType.Validation)
{
	public static ValidationError FromResults(IEnumerable<Result> results)
		=> new(results.Where(result => result.IsFailure).Select(result => result.Error).ToArray());
}