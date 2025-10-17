using ContentService.Application.Abstractions.Data;
using ContentService.Domain.Assignments;
using ContentService.Domain.AssignmentSubmissions;
using ContentService.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Infrastructure.Database;

public class ApplicationDbContext(
	DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
	public DbSet<Course> Courses { get; set; }
	public DbSet<Assignment> Assignments { get; set; }
	public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

		modelBuilder.HasDefaultSchema(Schemas.Default);
	}
}