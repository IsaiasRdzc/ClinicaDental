﻿namespace ClinicaDental.ApiService;

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
        catch (InvalidOperationException error)
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
        catch (InvalidOperationException error)
        {
            return Results.BadRequest(error.Message);
        }
        catch (UnauthorizedAccessException)
        {
            return Results.Unauthorized();
        }
        catch (Exception)
        {
            return Results.StatusCode(500);
        }
    }
}
