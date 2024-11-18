using ClinicaDental.ApiService.Appointments.Services.Appointments;
using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries.Appointments;

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

// Registries
builder.Services.AddTransient<AppointmentRegistry>();
builder.Services.AddTransient<ScheduleRegistry>();
builder.Services.AddTransient<DoctorRegistry>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();

// Mappings
app.MapDefaultEndpoints();
app.MapControllers();
app.MapAppointmentsEndpoints();

// Initialize the database
await app.InitializeDatabase();

await app.RunAsync();
