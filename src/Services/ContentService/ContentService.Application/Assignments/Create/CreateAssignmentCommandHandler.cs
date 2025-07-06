using ContentService.Domain.Assignments;
using ContentService.SharedKernel;
using ContentService.Application.Abstractions.Data;
using ContentService.Application.Courses.GetById;
using ContentService.Application.Messaging;

namespace ContentService.Application.Assignments.Create;

public class CreateAssignmentCommandHandler(
	IQueryHandler<GetCourseByIdQuery, CourseResponse> getCourseHandler,
	IApplicationDbContext dbContext)
	: ICommandHandler<CreateAssignmentCommand, CreateAssignmentResponse>
{
	public async Task<Result<CreateAssignmentResponse>> Handle(
		CreateAssignmentCommand command,
		CancellationToken cancellationToken)
	{
		var query = new GetCourseByIdQuery(command.CourseId);
		
		var result = await getCourseHandler.Handle(query, cancellationToken);
		
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
		
		// assignment.Raise(new AssignmentCreatedDomainEvent(assignment.Id));
		
		await dbContext.Assignments.AddAsync(assignment, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);
		
		return Result.Success(new CreateAssignmentResponse(assignment));
	}
}