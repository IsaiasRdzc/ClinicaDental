namespace ClinicaDental.ApiService.Login;

using ClinicaDental.ApiService;
using ClinicaDental.ApiService.DataBase.Models.Login;

using Microsoft.AspNetCore.Mvc;

public static class LoginEndpoints
{
    public static void MapLoginEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/login");

        group.MapPost(string.Empty, LoginAsDoctor);
    }

    public static async Task<IResult> LoginAsDoctor(
        Account account,
        AccountsManager accountmanager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await accountmanager.AttemptLogin(account));
    }
}
