namespace ClinicaDental.ApiService.DataBase;

using Npgsql;
using Polly;
using Polly.Retry;

public static class AppDbContextInitializer
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
