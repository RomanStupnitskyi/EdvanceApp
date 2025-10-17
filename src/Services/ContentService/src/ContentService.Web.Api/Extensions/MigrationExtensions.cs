using System.Diagnostics.CodeAnalysis;
using ContentService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Web.Api.Extensions;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
public static class MigrationExtensions
{
	public static void ApplyMigrations(this IApplicationBuilder app)
	{
		ArgumentNullException.ThrowIfNull(app);
		
		using var scope = app.ApplicationServices.CreateScope();

		using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

		dbContext.Database.Migrate();
	}
}