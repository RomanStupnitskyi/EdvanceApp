using ContentService.Domain.Courses;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.Extensions.Caching.Hybrid;

namespace ContentService.Application.Courses.Create;

internal sealed class CreateCourseCommandHandler(
	HybridCache cache,
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
		
		await dbContext.Courses.AddAsync(course, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);
		
		string cacheKey = $"course:{course.Id}";
		await cache.SetAsync(cacheKey, course, cancellationToken: cancellationToken);
		
		return Result.Success(new CreateCourseResponse(course));
	}
}
