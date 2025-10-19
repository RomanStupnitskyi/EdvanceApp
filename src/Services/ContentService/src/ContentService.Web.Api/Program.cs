using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ContentService.Application;
using ContentService.Infrastructure;
using ContentService.Web.Api;
using ContentService.Web.Api.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
	loggerConfiguration.ReadFrom.Configuration(context.Configuration));

builder.Services
	.AddApplication()
	.AddMassTransit(builder.Configuration)
	.AddPresentation()
	.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

WebApplication app = builder.Build();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

await app.RunAsync();

namespace ContentService.Web.Api
{
	[SuppressMessage("ReSharper", "PartialTypeWithSinglePart")]
	[SuppressMessage("Maintainability", "CA1515:Consider making public types internal")]
	public partial class Program;
}
