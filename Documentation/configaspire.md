# General Configuration of the Project
## Understand the .NET Aspire solution structure
The solution consists of the following projects:

- **AspireSample.ApiService:** An ASP.NET Core Minimal API project is used to provide data to the front end. This project depends on the shared AspireSample.ServiceDefaults project.
- **AspireSample.AppHost:** An orchestrator project designed to connect and configure the different projects and services of your app. The orchestrator should be set as the Startup project, and it depends on the AspireSample.ApiService and AspireSample.Web projects.
- **AspireSample.ServiceDefaults:** A .NET Aspire shared project to manage configurations that are reused across the projects in your solution related to resilience, service discovery, and telemetry.
- **AspireSample.Web:** An ASP.NET Core Blazor App project with default .NET Aspire service configurations, this project depends on the AspireSample.ServiceDefaults project. For more information, see .NET Aspire service defaults.



```mathematica
└───📂 AspireSample
     ├───📂 AspireSample.ApiService
     │    ├───📂 Properties
     │    ├─── appsettings.Development.json
     │    ├─── appsettings.json
     │    ├─── AspireSample.ApiService.csproj
     │    └─── Program.cs
     ├───📂 AspireSample.AppHost
     │    ├───📂 Properties
     │    ├─── appsettings.Development.json
     │    ├─── appsettings.json
     │    ├─── AspireSample.AppHost.csproj
     │    └─── Program.cs
     ├───📂 AspireSample.ServiceDefaults
     │    ├─── AspireSample.ServiceDefaults.csproj
     │    └─── Extensions.cs
     ├───📂 AspireSample.Web
     │    ├───📂 Components
     │    │    ├───📂 Layout
     │    │    ├───📂 Pages
     │    │    ├─── _Imports.razor
     │    │    ├─── App.razor
     │    │    └─── Routes.razor
     │    ├───📂 Properties
     │    ├───📂 wwwroot
     │    ├─── appsettings.Development.json
     │    ├─── appsettings.json
     │    ├─── AspireSample.Web.csproj
     │    ├─── Program.cs
     │    └─── WeatherApiClient.cs
     └─── AspireSample.sln
```
In this project, the default website in **AspireSample.Web** will not be necessary, as a custom Angular project will be used for the user interface. Thus, the frontend component will be managed by the specifically created Angular application, which will replace the basic web functionality of the initial project. This means that the Angular project can replace the default web folder, integrating in its place and connecting with the backend services in **AspireSample**.

Before integrating the Angular project with the main repository, it is necessary to delete the `.git` folder within the Angular project directory. This folder is created by default when the Angular project is initialized, resulting in a standalone Git repository. Removing this `.git` folder will prevent conflicts when integrating the Angular project into the main repository, ensuring a seamless version control process.

## Adding Essential Dependencies for Project Development

### Dependencies for the AppHost

To work with the Angular project and a PostgreSQL database, it is necessary to add the following dependencies directly in the **AspireSample.AppHost.csproj** file:

```xml
<ItemGroup>
    <PackageReference Include="Aspire.Hosting.AppHost" Version="8.2.2" />
    <PackageReference Include="Aspire.Hosting.NodeJs" Version="8.2.2" />
    <PackageReference Include="Aspire.Hosting.PostgreSQL" Version="8.2.2" />
</ItemGroup>

```

Adding these packages in the `.csproj` file enables **AspireSample.AppHost** to serve the Angular application through Node.js and connect to a PostgreSQL database, ensuring the orchestrator can effectively manage both frontend and database interactions.

### Dependencies for the ApiService

To ensure the proper functioning of the backend in **AspireSample.ApiService**, the following dependencies must be added to the **AspireSample.ApiService.csproj** file within an `<ItemGroup>` block:

```xml
<ItemGroup>
    <PackageReference Include="Aspire.Npgsql.EntityFrameworkCore.PostgreSQL" Version="8.2.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.10" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.9.0" />
    <PackageReference Include="Polly" Version="8.4.2" />
</ItemGroup>
```

In the launch settings configuration for the ASP.NET Core project, specifically in the **launchSettings.json** file, the following line should be added under the **http** profile:

```json
"launchUrl": "swagger"  // <-- Add this line here
```

The complete **http** profile will look like this:

```json
"profiles": {
    "http": {
        "commandName": "Project",
        "dotnetRunMessages": true,
        "launchBrowser": true,
        "launchUrl": "swagger",  // <-- Add this line here
        "applicationUrl": "http://localhost:5347",
        "environmentVariables": {
            "ASPNETCORE_ENVIRONMENT": "Development"
        }
    }
}
```

Adding the **"launchUrl": "swagger"** line specifies that when the application starts, it will automatically navigate to the Swagger UI, allowing for easy access to the API documentation and testing capabilities right from the browser. This configuration enhances the development experience by streamlining the process of interacting with the API endpoints.

### Purpose of Each Dependency:

1. **Aspire.Npgsql.EntityFrameworkCore.PostgreSQL**: This package enables integration with PostgreSQL, providing the necessary tools for working with the database within the Entity Framework Core context.

2. **Microsoft.EntityFrameworkCore**: This is essential for implementing Object-Relational Mapping (ORM) functionality, which simplifies data manipulation and database interactions.

3. **Swashbuckle.AspNetCore**: This package generates API documentation using Swagger, enhancing the accessibility and understanding of the API endpoints for developers and users.

4. **Polly**: This library implements resilience patterns, such as retry policies and circuit breakers, which help manage failures effectively and improve the robustness of the service.


## Configuration of `Program.cs` in AppHost

This file configures the distributed application for a dental clinic management project. Each section of the `Program.cs` configuration is explained below:

### 1. Initialize the Distributed Application Builder
```csharp
var builder = DistributedApplication.CreateBuilder(args);
```
This code initializes a `DistributedApplication`, allowing configuration and management of distributed services and databases from a central application. The application receives the startup arguments (`args`) in the builder.

### 2. Configure PostgreSQL with PgAdmin and PgWeb Services
```csharp
var postgres = builder.AddPostgres("Postgres")
    .WithPgAdmin()
    .WithPgWeb();
```
Here, PostgreSQL is configured as the database service using `AddPostgres`. 
- `WithPgAdmin()` enables PostgreSQL’s graphical management service (PgAdmin).
- `WithPgWeb()` allows managing the database through a web interface (PgWeb), facilitating database administration.

### 3. Create the `ClinicaDentalDb` Database
```csharp
var postgresDb = postgres.AddDatabase("ClinicaDentalDb");
```
`AddDatabase` creates a database named **ClinicaDentalDb** within the PostgreSQL service, providing a specific instance for the dental clinic project.

### 4. Configure the `ApiService` API Service
```csharp
var apiService = builder.AddProject<Projects.ClinicaDental_ApiService>("ApiService")
    .WithReference(postgresDb);
```
This configures the **API** service for the project:
- `AddProject` registers the project `ClinicaDental_ApiService` as a service named **ApiService**.
- `WithReference(postgresDb)` specifies that this API service depends on the **ClinicaDentalDb** database, ensuring connectivity and allowing the API to interact with stored data.

### 5. Configure the Web Application `WebApp`
```csharp
builder.AddNpmApp("WebApp", "../ClinicaDental.WebApp")
    .WithEnvironment("SERVER_URL", apiService.GetEndpoint("http"))
    .WithHttpEndpoint(targetPort: 4200);
```
The Angular web application is configured with the following steps:
- `AddNpmApp("WebApp", "../ClinicaDental.WebApp")`: Registers the web application, specifying its name **WebApp** and location in the project (`../ClinicaDental.WebApp`).
- `WithEnvironment("SERVER_URL", apiService.GetEndpoint("http"))`: Sets an environment variable `SERVER_URL` in the web application, pointing to the **HTTP endpoint** of the API service.
- `WithHttpEndpoint(targetPort: 4200)`: Defines HTTP port `4200`, the default port for Angular applications in development.

### 6. Final Project Build
```csharp
await builder.Build();
```
Finally, `Build()` compiles the entire configuration and prepares the distributed application for execution. This method ensures that services, databases, and connections between the frontend and backend are correctly constructed and ready for deployment.


 













