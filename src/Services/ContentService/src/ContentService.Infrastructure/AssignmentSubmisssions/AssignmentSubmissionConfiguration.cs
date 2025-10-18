using ContentService.Domain.Assignments;
using ContentService.Domain.AssignmentSubmissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentService.Infrastructure.AssignmentSubmisssions;

public class AssignmentSubmissionConfiguration : IEntityTypeConfiguration<AssignmentSubmission>
{
	public void Configure(EntityTypeBuilder<AssignmentSubmission> builder)
	{
		builder.HasKey(submission => submission.Id);
		
		builder.Property(submission => submission.StudentId)
			.IsRequired();
		
		builder.Property(submission => submission.Content)
			.IsRequired();
		
		builder.Property(submission => submission.SubmittedAt)
			.HasDefaultValueSql("CURRENT_TIMESTAMP");
		
		builder.Property(submission => submission.LastUpdatedAt)
			.IsRequired(false);
		
		builder.HasOne<Assignment>().WithMany().HasForeignKey(submission => submission.AssignmentId);
	}
}
