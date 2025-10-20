using ContentService.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using NSubstitute;

namespace Application.UnitTests.Abstractions;

public abstract class BaseTests
{
    protected readonly IApplicationDbContext DbContext;
    protected readonly HybridCache CacheMock;

    protected BaseTests()
    {
        var options = new DbContextOptionsBuilder<TestApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        DbContext = new TestApplicationDbContext(options);
        CacheMock = Substitute.For<HybridCache>();
    }
}
