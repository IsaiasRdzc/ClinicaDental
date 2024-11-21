using System.Text.Json;
using System.Text.Json.Serialization;

using ClinicaDental.ApiService.Appointments;
using ClinicaDental.ApiService.Appointments.Services;
using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;
using ClinicaDental.ApiService.ReynaldoPractices;
using ClinicaDental.ApiService.ReynaldoPractices.Services;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuci�n JsonSerializerOptions globalmente para Minimal APIs
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.WriteIndented = true;
});

// Add service defaults.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.AddNpgsqlDbContext<AppDbContext>("ClinicaDentalDb");
builder.Services.AddTransient<StoreKeeper>();
builder.Services.AddTransient<ClinicReceptionist>();
builder.Services.AddTransient<ClinicAgenda>();
builder.Services.AddTransient<ClinicAdmin>();
builder.Services.AddTransient<PaymentsAdmin>();

// Registries
builder.Services.AddTransient<SuppliesRegistry>();
builder.Services.AddTransient<AppointmentsRegistry>();
builder.Services.AddTransient<SchedulesRegistry>();
builder.Services.AddTransient<DoctorsRegistry>();
builder.Services.AddTransient<PaymentDetailRegistry>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();

// Mappings
app.MapDefaultEndpoints();
app.MapAppointmentsEndpoints();
app.MapSuppliesEndpoints();
app.MapPaymentEndpoints();

// Initialize the database
await app.InitializeDatabase();

await app.RunAsync();
