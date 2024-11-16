using ClinicaDental.ApiService.Appointments;
using ClinicaDental.ApiService.Appointments.Services;
using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;
using ClinicaDental.ApiService.ReynaldoPractices.Services;
using ClinicaDental.ApiService.ReynaldoPractices;

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
builder.Services.AddTransient<PaymentsAdmin>();

// Registries
builder.Services.AddTransient<AppointmentRegistry>();
builder.Services.AddTransient<ScheduleRegistry>();
builder.Services.AddTransient<DoctorRegistry>();
builder.Services.AddTransient<PaymentDetailRegistry>();

var app = builder.Build();



// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();


// Mappings
app.MapDefaultEndpoints();
app.MapControllers();
app.MapAppointmentsEndpoints();
app.MapPaymentEndpoints();

// Initialize the database
await app.InitializeDatabase();

await app.RunAsync();
