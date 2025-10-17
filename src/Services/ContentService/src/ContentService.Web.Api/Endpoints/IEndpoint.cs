using System.Diagnostics.CodeAnalysis;

namespace ContentService.Web.Api.Endpoints;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public interface IEndpoint
{
	void MapEndpoint(IEndpointRouteBuilder app);
}