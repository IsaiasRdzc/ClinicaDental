namespace ClinicaDental.ApiService.Appointments;

using ClinicaDental.ApiService.Appointments.Services;
using ClinicaDental.ApiService.DataBase.Models;

public static class SuppliesEndpoint
{
    public static void MapSuppliesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/supplies");

        group.MapPost("medical", AddMedicalSupply);
        group.MapPost("surgical", AddSurgicalSupply);
        //group.MapPost("cleaning", AddCleaningSupply);
        group.MapGet(string.Empty, GetSupplies);
        //group.MapGet("{id}", GetSupplyById);
        group.MapGet("{type}", GetSuppliesByType);
    }

    public static async Task<IResult> GetSupplies(StoreKeeper storeKeeper) 
    {
        var supplies = await storeKeeper.GetSupplies();
        return TypedResults.Ok(supplies);
    }

    public static async Task<IResult> GetSuppliesByType(
    SupplyType type,
    StoreKeeper storeKeeper)
    {
        var supplies = await storeKeeper.GetSuppliesByType(type);

        return TypedResults.Ok(supplies);
    }

    public static IResult AddMedicalSupply(MedicalSupply supply, StoreKeeper storeKeeper)
    {
        if (string.IsNullOrWhiteSpace(supply.MedicationType) || string.IsNullOrWhiteSpace(supply.LotNumber))
        {
            return Results.BadRequest("MedicationType y LotNumber son campos obligatorios.");
        }

        storeKeeper.AddSupply(supply);
        storeKeeper.SaveChanges();

        return Results.Created($"/api/supplies/medical/{supply.Id}", supply);
    }

    public static IResult AddSurgicalSupply(
       SurgicalSupply supply,
       StoreKeeper storeKeeper)
    {
        if (string.IsNullOrWhiteSpace(supply.SurgicalType) || string.IsNullOrWhiteSpace(supply.SterilizationMethod))
        {
            return Results.BadRequest("SurgicalType y SterilizationMethod son campos obligatorios.");
        }

        storeKeeper.AddSupply(supply);
        storeKeeper.SaveChanges();

        return Results.Created($"/api/supplies/surgical/{supply.Id}", supply);
    }

    public static IResult AddCleaningSupply(
       CleaningSupply supply,
       StoreKeeper storeKeeper)
    {
        if (string.IsNullOrWhiteSpace(supply.CleaningType) || string.IsNullOrWhiteSpace(supply.CleaningMethod))
        {
            return Results.BadRequest("CleaningType y CleaningMethod son campos obligatorios.");
        }

        storeKeeper.AddSupply(supply);
        storeKeeper.SaveChanges();

        return Results.Created($"/api/supplies/cleaning/{supply.Id}", supply);
    }
}
