namespace ClinicaDental.ApiService.Inventory.Services;

using ClinicaDental.ApiService.DataBase.Models.Inventory;

public static class SuppliesEndpoint
{
    public static void MapSuppliesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/supplies");

        group.MapPost("medical", AddMedicalSupply);
        group.MapPost("surgical", AddSurgicalSupply);
        group.MapPost("cleaning", AddCleaningSupply);
        group.MapGet(string.Empty, GetSupplies);
        group.MapGet("{id:int}", GetSupplyById);
        group.MapGet("{type:alpha}", GetSuppliesByType);
        group.MapDelete("{id:int}", RemoveSupply);
        group.MapPut("medical/{id:int}", UpdateMedicalSupply);
        group.MapPut("surgical/{id:int}", UpdateSurgicalSupply);
        group.MapPut("cleaning/{id:int}", UpdateCleaningSupply);
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
        try
        {
            var supplies = await storeKeeper.GetSuppliesByType(type);
            return TypedResults.Ok(supplies);
        }
        catch (InvalidOperationException error)
        {
            return Results.BadRequest(error.Message);
        }
        catch (ArgumentException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> AddMedicalSupply(
        MedicalSupply supply,
        StoreKeeper storeKeeper)
    {
        try
        {
            await storeKeeper.AddMedicalSupply(supply);

            return Results.Created($"/api/supplies/medical/{supply.Id}", supply);
        }
        catch (ArgumentNullException error)
        {
            return Results.BadRequest(error.Message);
        }
        catch (ArgumentException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> AddSurgicalSupply(
       SurgicalSupply supply,
       StoreKeeper storeKeeper)
    {
        try
        {
            await storeKeeper.AddSurgicalSupply(supply);
            return Results.Created($"/api/supplies/surgical/{supply.Id}", supply);
        }
        catch (ArgumentNullException error)
        {
            return Results.BadRequest(error.Message);
        }
        catch (ArgumentException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> AddCleaningSupply(
       CleaningSupply supply,
       StoreKeeper storeKeeper)
    {
        try
        {
            await storeKeeper.AddCleaningSupply(supply);
            return Results.Created($"/api/supplies/cleaning/{supply.Id}", supply);
        }
        catch (ArgumentNullException error)
        {
            return Results.BadRequest(error.Message);
        }
        catch (ArgumentException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static IResult GetSupplyById(
        int id,
        StoreKeeper storeKeeper)
    {
        try
        {
            var supply = storeKeeper.GetSupplyById(id);
            return Results.Ok(supply);
        }
        catch (KeyNotFoundException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> RemoveSupply(
    int id,
    StoreKeeper storeKeeper)
    {
        try
        {
            await storeKeeper.RemoveSupply(id);
            return Results.NoContent();
        }
        catch (KeyNotFoundException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> UpdateMedicalSupply(
        MedicalSupply updatedSupply,
        StoreKeeper storeKeeper)
    {
        try
        {
            await storeKeeper.UpdateMedicalSupply(updatedSupply);
            return Results.Ok("El suministro fue actualizado correctamente.");
        }
        catch (KeyNotFoundException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> UpdateSurgicalSupply(
        int id,
        SurgicalSupply updatedSupply,
        StoreKeeper storeKeeper)
    {
        try
        {
            await storeKeeper.UpdateSurgicalSupply(updatedSupply);
            return Results.Ok("El suministro fue actualizado correctamente.");
        }
        catch (KeyNotFoundException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> UpdateCleaningSupply(
        int id,
        CleaningSupply updatedSupply,
        StoreKeeper storeKeeper)
    {
        try
        {
            await storeKeeper.UpdateCleaningSupply(updatedSupply);
            return Results.Ok("El suministro fue actualizado correctamente.");
        }
        catch (KeyNotFoundException error)
        {
            return Results.BadRequest(error.Message);
        }
    }
}
