using ContentService.Application.Abstractions.Data;
using ContentService.Domain.Assignments;
using ContentService.Domain.AssignmentSubmissions;
using ContentService.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace Application.UnitTests.Abstractions;

public class TestApplicationDbContext(DbContextOptions options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }
}
