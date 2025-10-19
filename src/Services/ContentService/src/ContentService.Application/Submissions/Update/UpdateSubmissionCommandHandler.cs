using System.Diagnostics.CodeAnalysis;
using ContentService.Domain.AssignmentSubmissions;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Application.Submissions.Update;

internal sealed class UpdateSubmissionCommandHandler(
	IApplicationDbContext dbContext)
	: ICommandHandler<UpdateSubmissionCommand, UpdateSubmissionResponse>
{
	[SuppressMessage("ReSharper", "InvertIf")]
	public async Task<Result<UpdateSubmissionResponse>> Handle(
		UpdateSubmissionCommand command,
		CancellationToken cancellationToken)
	{
		AssignmentSubmission? submission = await dbContext.AssignmentSubmissions
			.SingleOrDefaultAsync(submission => submission.Id == command.SubmissionId, cancellationToken)
			;

		if (submission is null)
			return Result.Failure<UpdateSubmissionResponse>(
				AssignmentSubmissionErrors.NotFound(command.SubmissionId));

		if (submission.Content != command.Content && command.Content != null)
		{
			submission.Content = command.Content;
			submission.LastUpdatedAt = DateTime.UtcNow;
			
			await dbContext.SaveChangesAsync(cancellationToken);
		}
		
		return new UpdateSubmissionResponse(submission);
	}
}
