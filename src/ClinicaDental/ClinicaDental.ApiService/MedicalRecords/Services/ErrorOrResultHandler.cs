namespace ClinicaDental.ApiService.MedicalRecords.Services;

public static class ErrorOrResultHandler
{
    public static async Task<IResult> HandleRequest(Func<Task> operation)
    {
        try
        {
            await operation();
            return Results.Ok();
        }
        catch (ArgumentException error)
        {
            return Results.BadRequest(error.Message);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
        catch (UnauthorizedAccessException)
        {
            return Results.Unauthorized();
        }
    }

    private static async Task<IResult> HandleRequest<T>(Func<Task<T>> operation)
    {
        try
        {
            var result = await operation();
            return Results.Ok(result);
        }
        catch (ArgumentException error)
        {
            return Results.BadRequest(error.Message);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
        catch (UnauthorizedAccessException)
        {
            return Results.Unauthorized();
        }
    }
}
