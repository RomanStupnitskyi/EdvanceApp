using Application.UnitTests.Abstractions;
using Application.UnitTests.Extensions;
using ContentService.Application.Courses.GetById;
using ContentService.Domain.Courses;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Courses;

public class GetCourseByIdQueryTests : BaseTests
{
    private static readonly GetCourseByIdQuery Query = new (Guid.NewGuid());
    private readonly GetCourseByIdQueryHandler _handler;
    
    public GetCourseByIdQueryTests()
    {
        _handler = new GetCourseByIdQueryHandler(DbContext, CacheMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_When_CourseDoesNotExist()
    {
        // Act
        var result = await _handler.Handle(Query, CancellationToken.None);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CourseErrors.NotFound(Query.CourseId));
    }

    [Fact]
    public async Task Handle_Should_ReturnCourseUsingCache_When_CourseExists()
    {
        // Arrange
        string cachedCourseKey = $"course:{Query.CourseId}";
        var course = new Course
        {
            Id = Query.CourseId,
            Title = "Test Course",
            Description = "Test Description",
            IsVisible = true
        };
        CacheMock.SetupGetOrCreateAsync(Arg.Any<string>(), course);
        await DbContext.Courses.AddAsync(course);
        await DbContext.SaveChangesAsync(CancellationToken.None);
        
        // Act
        var result = await _handler.Handle(Query, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        await CacheMock.AssertGetOrCreateAsyncCalledAsync<Course>(cachedCourseKey, 1);
        result.Value.Id.Should().Be(course.Id);
        result.Value.Title.Should().Be(course.Title);
        result.Value.Description.Should().Be(course.Description);
        result.Value.IsVisible.Should().Be(course.IsVisible);
    }
}
