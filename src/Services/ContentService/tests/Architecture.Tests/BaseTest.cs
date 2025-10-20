using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ContentService.Application.Messaging;
using ContentService.Domain.Courses;
using ContentService.Infrastructure.Database;
using ContentService.Web.Api;

namespace Architecture.Tests;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Course).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;
    protected static readonly Assembly WebApiAssembly = typeof(Program).Assembly;
}