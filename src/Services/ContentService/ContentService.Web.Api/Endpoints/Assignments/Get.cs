using ContentService.Application.Assignments.Get;
using ContentService.Application.Messaging;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Assignments;

public class Get : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("/assignments", async (
			IQueryHandler<GetAssignmentQuery, List<GetAssignmentResponse>> handler,
			CancellationToken cancellationToken) =>
		{
			var result = await handler.Handle(new GetAssignmentQuery(), cancellationToken);

			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Assignments);
	}
}