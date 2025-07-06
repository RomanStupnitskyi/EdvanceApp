using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ContentService.Application;
using ContentService.Infrastructure;
using ContentService.Web.Api;
using ContentService.Web.Api.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// builder.services.AddSwaggerGenWithAuthentication(builder.configuration);

builder.Host.UseSerilog((context, loggerConfiguration) =>
	loggerConfiguration.ReadFrom.Configuration(context.Configuration));

builder.Services
	.AddApplication()
	.AddMassTransit(builder.Configuration)
	.AddPresentation()
	.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.MapHealthChecks("health", new HealthCheckOptions
{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

// app.UseAuthentication();

// app.UseAuthorization();

await app.RunAsync();

namespace ContentService.Web.Api
{
	[SuppressMessage("ReSharper", "PartialTypeWithSinglePart")]
	public partial class Program;
}