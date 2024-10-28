namespace ClinicaDental.ApiService.Appointments;

using ClinicaDental.ApiService.Appointments.Services;
using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Models.Supplies;
using ClinicaDental.ApiService.DataBase.Registries;

public static class SuppliesEndpoint
{
    public static void MapSupplyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/supplies");

        group.MapPost("medical", AddMedicalSupply);
    }

    public static async Task<IResult> AddMedicalSupply(
        MedicalSupply medicalSupply,
        SuppliesRegistry suppliesRegistry)
    {
        await suppliesRegistry.AddMedicalSupply(medicalSupply);
        return Results.Ok();
    }

}
