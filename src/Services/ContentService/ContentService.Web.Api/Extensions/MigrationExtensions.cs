﻿using ContentService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ContentService.Web.Api.Extensions;

public static class MigrationExtensions
{
	public static void ApplyMigrations(this IApplicationBuilder app)
	{
		using var scope = app.ApplicationServices.CreateScope();

		using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

		dbContext.Database.Migrate();
	}
}