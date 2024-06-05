using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MetricsProvider>();
builder.Services.ConfigureDatabase(builder.Configuration.GetConnectionString("AppDatabase") ?? throw new ArgumentNullException());

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/metrics", async (MetricsProvider metricsProvider) => await metricsProvider.GetMetricsAsync()).WithOpenApi();

app.Run();