namespace ClinicaDental.ApiService.Login;

using ClinicaDental.ApiService;

public static class LoginEndpoints
{
    public static void MapLoginEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/login");

        group.MapPost(string.Empty, LoginAsDoctor);
    }

    public static async Task<IResult> LoginAsDoctor(
        string username,
        string password,
        AccountsManager accountmanager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await accountmanager.AttemptLogin(username, password));
    }
}
