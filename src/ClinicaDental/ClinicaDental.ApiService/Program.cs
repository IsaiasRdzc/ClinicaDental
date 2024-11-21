using System.Text.Json;
using System.Text.Json.Serialization;

using ClinicaDental.ApiService.Appointments;
using ClinicaDental.ApiService.Appointments.Services;
using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Registries;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configución JsonSerializerOptions globalmente para Minimal APIs
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
builder.Services.AddTransient<AppointmentScheduler>();
builder.Services.AddTransient<AppointmentCalendar>();
builder.Services.AddTransient<WorkScheduleAdmin>();
builder.Services.AddTransient<StoreKeeper>();

// Registries
builder.Services.AddTransient<AppointmentRegistry>();
builder.Services.AddTransient<ScheduleRegistry>();
builder.Services.AddTransient<DoctorRegistry>();
builder.Services.AddTransient<SuppliesRegistry>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();

// Mappings
app.MapDefaultEndpoints();
app.MapAppointmentsEndpoints();
app.MapSuppliesEndpoints();

// Initialize the database
await app.InitializeDatabase();

await app.RunAsync();
