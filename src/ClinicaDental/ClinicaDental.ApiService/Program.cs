using System.Text.Json;
using System.Text.Json.Serialization;

using ClinicaDental.ApiService.Appointments.Services;
using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Registries.Appointments;
using ClinicaDental.ApiService.DataBase.Registries.Inventory;
using ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;
using ClinicaDental.ApiService.Inventory.Services;
using ClinicaDental.ApiService.MedicalRecords.Services;
using ClinicaDental.ApiService.ReynaldoPractices;
using ClinicaDental.ApiService.ReynaldoPractices.Services;

using ClinicaDental.ApiService.Purchases;
using ClinicaDental.ApiService.Materials;
using ClinicaDental.ApiService.Suppliers;
using ClinicaDental.ApiService.DataBase.Registries.Purchases;

using ClinicaDental.ApiService.Purchases.Services;
using ClinicaDental.ApiService.Materials.Services;
using ClinicaDental.ApiService.Suppliers.Services;

using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Configurar JsonSerializerOptions globalmente para Minimal APIs
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.SerializerOptions.WriteIndented = true;
});

// Configurar la conexión a la base de datos (PostgreSQL)
builder.AddNpgsqlDbContext<AppDbContext>("ClinicaDentalDb");

// Agregar servicios predeterminados y componentes adicionales
builder.AddServiceDefaults();

// Servicios generales de la aplicación
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Servicios específicos
builder.Services.AddTransient<StoreKeeper>();
builder.Services.AddTransient<ClinicReceptionist>();
builder.Services.AddTransient<ClinicAgenda>();
builder.Services.AddTransient<ClinicAdmin>();
builder.Services.AddTransient<PaymentsAdmin>();

builder.Services.AddTransient<MedicalInformationManager>();

// Servicios y managers para Purchases, Materials y Suppliers
builder.Services.AddScoped<PurchasesRegistry>();
builder.Services.AddScoped<PurchaseManager>();

builder.Services.AddScoped<MaterialsRegistry>();
builder.Services.AddScoped<MaterialManager>();

builder.Services.AddScoped<SuppliersRegistry>();
builder.Services.AddScoped<SupplierManager>();

// Registries específicos
builder.Services.AddTransient<SuppliesRegistry>();
builder.Services.AddTransient<AppointmentsRegistry>();
builder.Services.AddTransient<SchedulesRegistry>();
builder.Services.AddTransient<DoctorsRegistry>();
builder.Services.AddTransient<PaymentDetailRegistry>();

builder.Services.AddTransient<PurchasesDataRegistry>();
builder.Services.AddTransient<PurchasesRegistry>();
builder.Services.AddTransient<MaterialsRegistry>();
builder.Services.AddTransient<SuppliersRegistry>();

builder.Services.AddTransient<MedicalRecordsRegistry>();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Mapeo de endpoints
app.MapDefaultEndpoints();
app.MapAppointmentsEndpoints();
app.MapSuppliesEndpoints();
app.MapMedicalRecordsEndpoints();
app.MapPaymentEndpoints();

// Mapeo adicional de endpoints para Purchases, Materials y Suppliers
app.MapPurchasesEndpoints();
app.MapMaterialsEndpoints();
app.MapSuppliersEndpoints();

// Inicializar la base de datos
await app.InitializeDatabase();

await app.RunAsync();
