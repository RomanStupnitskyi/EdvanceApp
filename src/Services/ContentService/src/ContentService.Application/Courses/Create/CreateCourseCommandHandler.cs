using ContentService.Domain.Courses;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Messaging;
using Microsoft.Extensions.Caching.Hybrid;

namespace ContentService.Application.Courses.Create;

public class CreateCourseCommandHandler(
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
		
		await dbContext.Courses.AddAsync(course, cancellationToken).ConfigureAwait(false);
		await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		
		string cacheKey = $"course:{course.Id}";
		await cache.SetAsync(cacheKey, course, cancellationToken: cancellationToken).ConfigureAwait(false);
		
		return Result.Success(new CreateCourseResponse(course));
	}
}
