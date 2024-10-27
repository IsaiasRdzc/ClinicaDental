using ClinicaDental.ApiService.Appointments;
using ClinicaDental.ApiService.Appointments.Services;
using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Registries;

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
builder.Services.AddTransient<AppointmentScheduler>();
builder.Services.AddTransient<AppointmentCalendar>();

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
