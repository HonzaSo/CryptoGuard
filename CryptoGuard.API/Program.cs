using CryptoGuard.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureDi(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();