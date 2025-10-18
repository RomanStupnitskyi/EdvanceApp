using System.Diagnostics.CodeAnalysis;
using ContentService.Application.Assignments.GetMany;
using ContentService.Application.Messaging;
using ContentService.SharedKernel;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Assignments;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public class GetMany : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("/assignments", async (
			IQueryHandler<GetAssignmentQuery, List<GetAssignmentResponse>> handler,
			CancellationToken cancellationToken) =>
		{
			Result<List<GetAssignmentResponse>> result = await handler.Handle(new GetAssignmentQuery(), cancellationToken).ConfigureAwait(false);

			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Assignments);
	}
}