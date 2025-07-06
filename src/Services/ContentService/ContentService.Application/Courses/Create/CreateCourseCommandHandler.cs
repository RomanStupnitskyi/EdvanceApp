using ContentService.Domain.Courses;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;

namespace ContentService.Application.Courses.Create;

public class CreateCourseCommandHandler(
	IApplicationDbContext dbContext)
	: ICommandHandler<CreateCourseCommand, CreateCourseResponse>
{
	public async Task<Result<CreateCourseResponse>> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
	{
		var course = new Course
		{
			Title = command.Title,
			Description = command.Description,
			IsVisible = command.IsVisible,
			CreatedBy = command.CreatedBy
		};
		
		// course.Raise(new CourseCreatedDomainEvent(course.Id));
		
		await dbContext.Courses.AddAsync(course, cancellationToken);
		
		await dbContext.SaveChangesAsync(cancellationToken);
		
		return Result.Success(new CreateCourseResponse(course));
	}
}