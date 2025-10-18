using ContentService.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentService.Infrastructure.Courses;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
	public void Configure(EntityTypeBuilder<Course> builder)
	{
		builder.HasKey(course => course.Id);

		builder.Property(course => course.Title)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(course => course.Description)
			.HasMaxLength(500);

		builder.Property(course => course.CreatedAt)
			.HasDefaultValueSql("CURRENT_TIMESTAMP");

		builder.Property(course => course.LastModifiedAt)
			.IsRequired(false);
	}
}
