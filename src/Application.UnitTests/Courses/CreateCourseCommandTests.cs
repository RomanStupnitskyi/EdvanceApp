using Application.UnitTests.Abstractions;
using Application.UnitTests.Extensions;
using ContentService.Application.Courses.Create;
using ContentService.Domain.Courses;
using FluentAssertions;
using FluentValidation.TestHelper;
using NSubstitute;

namespace Application.UnitTests.Courses;

public class CreateCourseCommandTests : BaseTests
{
    private static readonly CreateCourseCommand Command = new()
    {
        CourseId = Guid.NewGuid(),
        Title = "Test Course",
        Description = "Test Course Description",
        IsVisible = true,
        CreatedBy = Guid.NewGuid()
    };
    private readonly CreateCourseValidator _validator;
    private readonly CreateCourseCommandHandler _handler;
    
    public CreateCourseCommandTests()
    {
        _handler = new CreateCourseCommandHandler(CacheMock, DbContext);
        _validator = new CreateCourseValidator();
    }
    
    [Fact]
    public async Task Handle_Should_ReturnError_When_TitleIsEmpty()
    {
        // Arrange
        var invalidCommand = Command with { Title = string.Empty };

        // Act
        var result = await _validator.TestValidateAsync(invalidCommand);

        // Assert
        result
            .ShouldHaveValidationErrorFor(course => course.Title)
            .WithErrorCode("TitleRequired");
    }
    
    [Fact]
    public async Task Handle_Should_ReturnError_When_TitleExceedsMaxLength()
    {
        // Arrange
        var invalidCommand = Command with { Title = new string('A', 101) };

        // Act
        var result = await _validator.TestValidateAsync(invalidCommand);

        // Assert
        result
            .ShouldHaveValidationErrorFor(course => course.Title)
            .WithErrorCode("TitleMaxLengthExceeded");
    }

    [Fact]
    public async Task Handle_Should_ReturnError_When_DescriptionIsEmpty()
    {
        // Arrange
        var invalidCommand = Command with { Description = string.Empty };

        // Act
        var result = await _validator.TestValidateAsync(invalidCommand);

        // Assert
        result
            .ShouldHaveValidationErrorFor(course => course.Description)
            .WithErrorCode("DescriptionRequired");
    }

    [Fact]
    public async Task Handle_Should_ReturnError_When_DescriptionExceedsMaxLength()
    {
        // Arrange
        var invalidCommand = Command with { Description = new string('A', 501) };
        // Act
        var result = await _validator.TestValidateAsync(invalidCommand);
        // Assert
        result
            .ShouldHaveValidationErrorFor(course => course.Description)
            .WithErrorCode("DescriptionMaxLengthExceeded");
    }
    
    [Fact]
    public async Task Handle_Should_CreateCourseWithCache_When_CommandIsValid()
    {
        // Arrange
        var validCommand = Command;
        string cachedCourseKey = $"course:{validCommand.CourseId}";
        CacheMock.SetupSetAsync<Course>(cachedCourseKey);

        // Act
        var result = await _handler.Handle(validCommand, CancellationToken.None);
        var createdCourse = await DbContext.Courses.FindAsync(result.Value.Id);

        // Assert
        result.IsSuccess.Should().BeTrue();
        createdCourse.Should().NotBeNull();
        await CacheMock.AssertSetAsyncCalledAsync<Course>(cachedCourseKey, 1);
        createdCourse.Title.Should().Be(validCommand.Title);
        createdCourse.Description.Should().Be(validCommand.Description);
        createdCourse.IsVisible.Should().Be(validCommand.IsVisible);
        createdCourse.CreatedBy.Should().Be(validCommand.CreatedBy);
    }
}
