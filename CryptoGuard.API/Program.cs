using CryptoGuard.API.Middlewares;
using CryptoGuard.Application;
using CryptoGuard.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.AddInfrastructureDi(builder.Configuration);
builder.Services.AddApplicationDi();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();