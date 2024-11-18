using ClinicaDental.ApiService.Appointments.Services;
using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Registries.Appointments;
using ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;
using ClinicaDental.ApiService.MedicalRecords.Services;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

// Services
builder.AddNpgsqlDbContext<AppDbContext>("ClinicaDentalDb");
builder.Services.AddTransient<ClinicReceptionist>();
builder.Services.AddTransient<ClinicAgenda>();
builder.Services.AddTransient<ClinicAdmin>();

builder.Services.AddTransient<MedicalRecordsManager>();

// Registries
builder.Services.AddTransient<AppointmentRegistry>();
builder.Services.AddTransient<ScheduleRegistry>();
builder.Services.AddTransient<DoctorRegistry>();

builder.Services.AddTransient<MedicalRecordsRegistry>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();

// Mappings
app.MapDefaultEndpoints();
app.MapControllers();
app.MapAppointmentsEndpoints();
app.MapMedicalRecordsEndpoints();

// Initialize the database
await app.InitializeDatabase();

await app.RunAsync();
