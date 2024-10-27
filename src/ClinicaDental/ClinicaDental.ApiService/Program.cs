using ClinicaDental.ApiService.Appointments;
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

// Scoped Services
builder.AddNpgsqlDbContext<AppDbContext>("ClinicaDentalDb");

// Registries
builder.Services.AddScoped<AppointmentRegistry>();
builder.Services.AddScoped<DoctorRegistry>();

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
