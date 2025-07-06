using ContentService.Domain.Assignments;
using ContentService.Domain.AssignmentSubmissions;
using ContentService.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Application.Abstractions.Data;

public interface IApplicationDbContext
{
	DbSet<Course> Courses { get; }
	DbSet<Assignment> Assignments { get; }
	DbSet<AssignmentSubmission> AssignmentSubmissions { get; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}