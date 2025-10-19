using ContentService.Domain.AssignmentSubmissions;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Application.Submissions.Delete;

internal sealed class DeleteSubmissionCommandHandler(
	IApplicationDbContext dbContext) : ICommandHandler<DeleteSubmissionCommand>
{
	public async Task<Result> Handle(
		DeleteSubmissionCommand command,
		CancellationToken cancellationToken)
	{
		AssignmentSubmission? submission = await dbContext.AssignmentSubmissions
			.SingleOrDefaultAsync(submission => submission.Id == command.SubmissionId, cancellationToken);

		if (submission is null)
			return Result.Failure(AssignmentSubmissionErrors.NotFound(command.SubmissionId));

		dbContext.AssignmentSubmissions.Remove(submission);
		await dbContext.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}
