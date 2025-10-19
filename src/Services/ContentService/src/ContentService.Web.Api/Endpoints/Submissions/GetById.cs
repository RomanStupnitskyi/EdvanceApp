using System.Diagnostics.CodeAnalysis;
using ContentService.Application.Messaging;
using ContentService.Application.Submissions.GetById;
using ContentService.SharedKernel;
using ContentService.Web.Api.Extensions;
using ContentService.Web.Api.Infrastructure;

namespace ContentService.Web.Api.Endpoints.Submissions;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public class GetById : IEndpoint
{
	public void MapEndpoint(IEndpointRouteBuilder app)
	{
		app.MapGet("submissions/{submissionId:guid}", async (
			Guid submissionId,
			IQueryHandler<GetSubmissionByIdQuery, GetSubmissionByIdResponse> handler,
			CancellationToken cancellationToken) =>
		{
			var query = new GetSubmissionByIdQuery(submissionId);

			Result<GetSubmissionByIdResponse> result = await handler.Handle(query, cancellationToken);

			return result.Match(Results.Ok, CustomResults.Problem);
		})
		.WithTags(Tags.Submissions);
	}
}
