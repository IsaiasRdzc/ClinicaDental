var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddMongoDB("Mongo")
    .WithMongoExpress();

var mongoDb = mongo.AddDatabase("MongoDb");

var apiService = builder.AddProject<Projects.ClinicaDental_ApiService>("ApiService")
    .WithReference(mongoDb);

builder.AddNpmApp("WebApp", "../ClinicaDental.WebApp")
    .WithEnvironment("SERVER_URL", apiService.GetEndpoint("http"))
    .WithHttpEndpoint(targetPort: 4200);

builder.Build().Run();
