using CryptoGuard.API.Middlewares;
using CryptoGuard.Application;
using CryptoGuard.Infrastructure;
using CryptoGuard.Infrastructure.BackgroundJobs;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddTransient<LoggingMiddleware>();

builder.Services.AddInfrastructureDi(builder.Configuration);
builder.Services.AddApplicationDi();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<UpdatePricesJob>(
    "UpdatePricesJob",
    job => job.ExecuteAsync(),
    Cron.Minutely);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.Run();