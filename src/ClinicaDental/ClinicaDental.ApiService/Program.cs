using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.AddNpgsqlDbContext<AppDbContext>("ClinicaDentalDb");
var app = builder.Build();

// Endpoint para obtener insumos de un tipo específico
app.MapGet("/supplies/{type}", async (string type, AppDbContext db) =>
{
    return type.ToLower() switch
    {
        "medical" => Results.Ok(await db.MedicalSupplies.ToListAsync()),
        "surgical" => Results.Ok(await db.SurgicalSupplies.ToListAsync()),
        "cleaning" => Results.Ok(await db.CleaningSupplies.ToListAsync()),
        _ => Results.BadRequest("Tipo de insumo no válido. Use 'medical', 'surgical', o 'cleaning'.")
    };
});

app.MapGet("/supplies", async (AppDbContext db) =>
{
    return Results.Ok(await db.Supplies.ToListAsync());
});

// Endpoint para agregar un nuevo MedicalSupply
app.MapPost("/medicalsupplies", async (MedicalSupply medicalSupply, AppDbContext db) =>
{
    db.MedicalSupplies.Add(medicalSupply);
    await db.SaveChangesAsync();
    return Results.Created("/medicalsupplies", medicalSupply);
});

// Endpoint para agregar un nuevo SurgicalSupply
app.MapPost("/surgicalsupplies", async (SurgicalSupply surgicalSupply, AppDbContext db) =>
{
    db.SurgicalSupplies.Add(surgicalSupply);
    await db.SaveChangesAsync();
    return Results.Created("/surgicalsupplies", surgicalSupply);
});

// Endpoint para agregar un nuevo CleaningSupply
app.MapPost("/cleaningsupplies", async (CleaningSupply cleaningSupply, AppDbContext db) =>
{
    db.CleaningSupplies.Add(cleaningSupply);
    await db.SaveChangesAsync();
    return Results.Created("/cleaningsupplies", cleaningSupply);
});


// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.MapDefaultEndpoints();
app.MapControllers();
await app.InitializeDatabase();

app.Run();
