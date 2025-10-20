using ContentService.Domain.Courses;
using Microsoft.Extensions.Caching.Hybrid;
using NSubstitute;
using NSubstitute.Core;

namespace Application.UnitTests.Extensions;

public static class NSubstituteHybridCacheExtensions
{
    public static ConfiguredCall SetupGetOrCreateAsync<T>(this HybridCache mockCache, string key, T expectedValue)
    {
        return mockCache.GetOrCreateAsync(
            key,
            Arg.Any<object>(),
            Arg.Any<Func<object, CancellationToken, ValueTask<T>>>(),
            Arg.Any<HybridCacheEntryOptions?>(), 
            Arg.Any<IEnumerable<string>?>(), 
            Arg.Any<CancellationToken>()
        ).Returns(expectedValue);
    }
  
    public static async Task AssertGetOrCreateAsyncCalledAsync<T>(this HybridCache mockCache, string key, int requiredNumberOfCalls)
    {
        await mockCache.Received(requiredNumberOfCalls).GetOrCreateAsync(
            key,
            Arg.Any<object>(),
            Arg.Any<Func<object, CancellationToken, ValueTask<T>>>(),
            null,
            null,
            Arg.Any<CancellationToken>()
        );
    }
}
