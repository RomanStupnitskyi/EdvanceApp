using ContentService.Domain.Assignments;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Courses.GetById;
using ContentService.Application.Messaging;
using Microsoft.Extensions.Caching.Hybrid;

namespace ContentService.Application.Assignments.Create;

public class CreateAssignmentCommandHandler(
	IQueryHandler<GetCourseByIdQuery, CourseByIdResponse> getCourseHandler,
	HybridCache cache,
	IApplicationDbContext dbContext)
	: ICommandHandler<CreateAssignmentCommand, CreateAssignmentResponse>
{
	public async Task<Result<CreateAssignmentResponse>> Handle(
		CreateAssignmentCommand command,
		CancellationToken cancellationToken)
	{
		var query = new GetCourseByIdQuery(command.CourseId);
		
		Result<CourseByIdResponse> result = await getCourseHandler.Handle(query, cancellationToken).ConfigureAwait(false);
		
		if (result.IsFailure)
			return Result.Failure<CreateAssignmentResponse>(
				AssignmentErrors.CourseNotFound(command.CourseId));
		
		var assignment = new Assignment
		{
			Id = Guid.NewGuid(),
			CourseId = command.CourseId,
			Title = command.Title,
			Description = command.Description,
			AllowLateSubmissions = command.AllowLateSubmissions,
			AllowResubmissions = command.AllowResubmissions,
			IsVisible = command.IsVisible,
			StartDate = command.StartDate,
			EndDate = command.EndDate
		};
		
		await dbContext.Assignments.AddAsync(assignment, cancellationToken).ConfigureAwait(false);
		await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		
		string cacheKey = $"assignment:{assignment.Id}";
		await cache.SetAsync(cacheKey, assignment, cancellationToken: cancellationToken).ConfigureAwait(false);
		
		return Result.Success(new CreateAssignmentResponse(assignment));
	}
}
