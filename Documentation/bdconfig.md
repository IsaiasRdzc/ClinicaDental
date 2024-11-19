
# Database Configuration in `Program.cs` for ApiService

This `Program.cs` file contains the essential database configurations for the dental clinic’s API service. Below are the specific steps for connecting and configuring the **ClinicaDentalDb** database.

### 1. Import the Database Namespace
```csharp
using ClinicaDental.ApiService.DataBase;
```
This line imports the namespace where the `AppDbContext` database context is defined, establishing the connection between the API service and the PostgreSQL database.

### 2. Configure the Database Context with PostgreSQL
```csharp
builder.AddNpgsqlDbContext<AppDbContext>("ClinicaDentalDb");
```
This line is the core of the database setup. The `AddNpgsqlDbContext` method configures `AppDbContext` (the context managing entities and communication with the database) with PostgreSQL:

- **`AppDbContext`**: Acts as the database manager in the application, translating code operations into queries for **ClinicaDentalDb**.
- **"ClinicaDentalDb"**: Specifies the configured PostgreSQL database name for the project.

This step ensures that all data operations in the API are correctly directed to the PostgreSQL database.

### 3. Initialize the Database on Application Startup
```csharp
await app.InitializeDatabase();
```
The `InitializeDatabase()` function ensures that the database is fully set up and configured when the application starts, including creating tables and applying migrations if needed. This step is critical to prevent errors in database interaction once requests start flowing in.



---

## Add a `AppDbContext` Class
The `AppDbContext` class is the database context for the **Clínica Dental** API project. It defines how the application interacts with the database and represents entities as database tables.

```csharp
namespace ClinicaDental.ApiService.DataBase;

using ClinicaDental.ApiService.DataBase.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Supply> Supplies { get; init; }
    public DbSet<Appointment> Appointments { get; init; }
}
```

## Add a `DataBaseInitializer` Class

The `DataBaseInitializer` class is a utility designed to ensure the database for the **Clínica Dental** API service is correctly created and initialized before handling requests. It includes a method that verifies the database is accessible, creating it if necessary and logging success or failure.
```csharp
namespace ClinicaDental.ApiService.DataBase
{
    using Microsoft.EntityFrameworkCore;

    internal static class DataBaseInitializer
    {
        public static async Task InitializeDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await Task.Delay(TimeSpan.FromSeconds(2));

            bool dbCreated = await AppDbContextInitializer.EnsureCreatedAsync(
                context,
                exception => app.Logger.LogInformation(exception, "Waiting for database..."));

            if (!dbCreated)
            {
                throw new TimeoutException("Could not create database.");
            }

            string dbName = context.Database.GetDbConnection().Database;
            app.Logger.LogInformation("Database '{Name}' created successfully", dbName);
        }
    }
}
```

## Add a `AppDbContextInitializer` Class
The `AppDbContextInitializer` class is responsible for ensuring that the database is created and ready for use in the **Clínica Dental** API service. It employs the Polly library to implement a retry policy, allowing the application to handle temporary issues when connecting to the database.
```csharp
namespace ClinicaDental.ApiService.DataBase;

using Npgsql;
using Polly;
using Polly.Retry;

public class AppDbContextInitializer
{
    public static Task<bool> EnsureCreatedAsync(AppDbContext context, Action<Exception> onException)
    {
        var retryPolicy = Policy
            .Handle<ServiceUnavailableException>()
            .WaitAndRetryAsync(
                retryCount: 5,
                _ => TimeSpan.FromSeconds(5));

        return EnsureCreatedAsync(context, onException, retryPolicy);
    }

    public static Task<bool> EnsureCreatedAsync(AppDbContext context, Action<Exception> onException, AsyncRetryPolicy retryPolicy)
    {
        return retryPolicy.ExecuteAsync(async () =>
        {
            try
            {
                return await context.Database.EnsureCreatedAsync();
            }
            catch (NpgsqlException exception) when (DatabaseNotReadyExceptionFilter(exception))
            {
                var exceptionWrap = new ServiceUnavailableException("Database not ready.", exception);
                onException.Invoke(exceptionWrap);
                throw exceptionWrap;
            }
        });
    }

    private static bool DatabaseNotReadyExceptionFilter(NpgsqlException exception)
    {
        if (exception is PostgresException postgresException)
        {
            return postgresException.SqlState == "57P03";
        }

        return exception.InnerException is EndOfStreamException;
    }
}
```


