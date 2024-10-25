var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("Postgres")
    .WithPgAdmin()
    .WithPgWeb();

var postgresDb = postgres.AddDatabase("ClinicaDentalDb");

var apiService = builder.AddProject<Projects.ClinicaDental_ApiService>("ApiService")
    .WithReference(postgresDb);

builder.AddNpmApp("WebApp", "../ClinicaDental.WebApp")
    .WithEnvironment("SERVER_URL", apiService.GetEndpoint("http"))
    .WithHttpEndpoint(targetPort: 4200);

builder.Build().Run();
