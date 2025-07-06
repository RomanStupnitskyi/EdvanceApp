using ContentService.Domain.Assignments;
using ContentService.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentService.Infrastructure.Assignments;

internal sealed class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
{
	public void Configure(EntityTypeBuilder<Assignment> builder)
	{
		builder.HasKey(assignment => assignment.Id);
		
		builder.Property(assignment => assignment.Title)
			.IsRequired()
			.HasMaxLength(200);
		
		builder.Property(assignment => assignment.Description)
			.HasMaxLength(1000);
		
		builder.Property(assignment => assignment.AllowLateSubmissions)
			.IsRequired();
		
		builder.Property(assignment => assignment.AllowResubmissions)
			.IsRequired();
		
		builder.Property(assignment => assignment.IsVisible)
			.IsRequired();
		
		builder.Property(assignment => assignment.EndDate)
			.IsRequired(false);
		
		builder.Property(assignment => assignment.CreatedAt)
			.HasDefaultValueSql("CURRENT_TIMESTAMP");
		
		builder.Property(assignment => assignment.LastModifiedAt)
			.IsRequired(false);
		
		builder.Property(assignment => assignment.LastModifiedBy)
			.IsRequired(false);
		
		builder.HasOne<Course>().WithMany().HasForeignKey(assignment => assignment.CourseId);
	}
}