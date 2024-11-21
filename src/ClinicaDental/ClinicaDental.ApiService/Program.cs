using System.Text.Json;
using System.Text.Json.Serialization;

using ClinicaDental.ApiService.Appointments;
using ClinicaDental.ApiService.Appointments.Services;
using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Registries.Appointments;
using ClinicaDental.ApiService.DataBase.Registries.Doctors;
using ClinicaDental.ApiService.DataBase.Registries.Inventory;
using ClinicaDental.ApiService.DataBase.Registries.Login;
using ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;
using ClinicaDental.ApiService.Inventory.Services;
using ClinicaDental.ApiService.Login;
using ClinicaDental.ApiService.MedicalRecords.Endpoints;
using ClinicaDental.ApiService.MedicalRecords.Services;
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
builder.Services.AddTransient<AccountsManager>();
builder.Services.AddTransient<MedicalRecordsManager>();
builder.Services.AddTransient<PatientsInformationManager>();

// Registries
builder.Services.AddTransient<SuppliesRegistry>();
builder.Services.AddTransient<AppointmentsRegistry>();
builder.Services.AddTransient<SchedulesRegistry>();
builder.Services.AddTransient<DoctorsRegistry>();
builder.Services.AddTransient<PaymentDetailRegistry>();
builder.Services.AddTransient<MedicalRecordsRegistry>();
builder.Services.AddTransient<AccountsRegistry>();
builder.Services.AddTransient<PatientsRegistry>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();

// Mappings
app.MapDefaultEndpoints();
app.MapAppointmentsEndpoints();
app.MapSuppliesEndpoints();
app.MapMedicalRecordsEndpoints();
app.MapPatientInformationEndpoints();
app.MapPaymentEndpoints();
app.MapLoginEndpoints();

// Initialize the database
await app.InitializeDatabase();

await app.RunAsync();
