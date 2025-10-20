using Application.UnitTests.Abstractions;
using Application.UnitTests.Extensions;
using ContentService.Application.Courses.Delete;
using ContentService.Domain.Courses;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Courses;

public class DeleteCourseCommandTests : BaseTests
{
    private static readonly DeleteCourseCommand Command = new(Guid.NewGuid());
    private readonly DeleteCourseCommandHandler _handler;
    
    public DeleteCourseCommandTests()
    {
        _handler = new DeleteCourseCommandHandler(CacheMock, DbContext);
    }
    
    public void Dispose()
    {
        (DbContext as TestApplicationDbContext)?.Dispose();
    }
    
    [Fact]
    public async Task Handle_Should_ReturnError_When_CourseNotFound()
    {
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(CourseErrors.NotFound(Command.CourseId));
    }

    [Fact]
    public async Task Handle_Should_DeleteCourse_When_Found()
    {
        // Arrange
        var course = new Course
        {
            Id = Command.CourseId,
            Title = "Test Course",
            Description = "Test Description"
        };
        await DbContext.Courses.AddAsync(course);
        await DbContext.SaveChangesAsync();
        
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        var deletedCourse = await DbContext.Courses.FindAsync(Command.CourseId);
        deletedCourse.Should().BeNull();
    }
    
    [Fact]
    public async Task Handle_Should_ClearCache_After_DeletingCourse()
    {
        // Arrange
        string cacheKey = $"course:{Command.CourseId}";
        CacheMock.SetupRemoveAsync(cacheKey);
        var course = new Course
        {
            Id = Command.CourseId,
            Title = "Test Course",
            Description = "Test Description"
        };
        await DbContext.Courses.AddAsync(course);
        await DbContext.SaveChangesAsync();
        
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        var deletedCourse = await DbContext.Courses.FindAsync(Command.CourseId);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        await CacheMock.AssertRemoveAsyncCalledAsync(cacheKey, 1);
        deletedCourse.Should().BeNull();
    }
}
