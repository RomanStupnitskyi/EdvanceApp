using System.Diagnostics.CodeAnalysis;

namespace ContentService.Web.Api.Endpoints;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public static class Tags
{
	public const string Courses = "Courses";
	public const string Assignments = "Assignments";
	public const string Submissions = "Submissions";
}