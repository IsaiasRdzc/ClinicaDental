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
