using ClinicaDental.ApiService.DataBase;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.AddNpgsqlDbContext<AppDbContext>("ClinicaDentalDb");
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.MapDefaultEndpoints();
app.MapControllers();
await app.InitializeDatabase();

app.Run();
