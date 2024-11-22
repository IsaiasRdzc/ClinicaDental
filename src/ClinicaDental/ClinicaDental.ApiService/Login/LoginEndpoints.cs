namespace ClinicaDental.ApiService.Login;

using ClinicaDental.ApiService;

public static class LoginEndpoints
{
    public static void MapLoginEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/login");

        group.MapPost(string.Empty, AuthenticateDoctorEndpoint);
    }

    public static async Task<IResult> AuthenticateDoctorEndpoint(
        LoginCredentials loginRequest,
        AccountsManager accountmanager)
    {
        return await ErrorOrResultHandler.HandleResult(async () =>
            await accountmanager.ResolveDoctorFromCredentials(loginRequest.Username, loginRequest.Password));
    }
}
