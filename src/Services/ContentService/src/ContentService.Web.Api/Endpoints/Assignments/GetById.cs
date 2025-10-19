using System.Diagnostics.CodeAnalysis;
using ContentService.Application.Assignments.GetById;
using ContentService.Application.Messaging;
using ContentService.SharedKernel;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Assignments;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public class GetById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("/assignments/{id:guid}", async (
			Guid id,
			IQueryHandler<GetAssignmentByIdQuery, AssignmentByIdResponse> handler,
			CancellationToken cancellationToken) =>
		{
			var query = new GetAssignmentByIdQuery(id);
		
			Result<AssignmentByIdResponse> result = await handler.Handle(query, cancellationToken);
		
			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Assignments);
	}
}
