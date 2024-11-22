namespace ClinicaDental.ApiService.MedicalRecords.ErrorRelated;

public static class ErrorOrResultHandler
{
    public static async Task<IResult> HandleResult(Func<Task> operation)
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
        catch (Exception)
        {
            return Results.StatusCode(500);
        }
    }

    public static async Task<IResult> HandleResult<T>(Func<Task<T>> operation)
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
        catch (Exception)
        {
            return Results.StatusCode(500);
        }
    }
}
