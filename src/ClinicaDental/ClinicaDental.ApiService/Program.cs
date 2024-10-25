using ClinicaDental.ApiService.DataBase;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.AddMongoDBClient("MongoDb");

var app = builder.Build();

var apiGroup = app.MapGroup("/api");

apiGroup.MapGet("/test", () =>
{
    return "Hello World!";
});

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.MapDefaultEndpoints();

app.Run();
