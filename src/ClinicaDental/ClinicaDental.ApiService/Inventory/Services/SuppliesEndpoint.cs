namespace ClinicaDental.ApiService.Inventory.Services;

using ClinicaDental.ApiService.DataBase.Models.Inventory;
using ClinicaDental.ApiService.MedicalRecords.Services;

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
        return await ErrorOrResultHandler.HandleResult(async () => await storeKeeper.GetSupplies());
    }

    public static async Task<IResult> GetSuppliesByType(
    SupplyType type,
    StoreKeeper storeKeeper)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await storeKeeper.GetSuppliesByType(type));
    }

    public static async Task<IResult> AddMedicalSupply(
        MedicalSupply supply,
        StoreKeeper storeKeeper)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await storeKeeper.AddMedicalSupply(supply));
    }

    public static async Task<IResult> AddSurgicalSupply(
       SurgicalSupply supply,
       StoreKeeper storeKeeper)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await storeKeeper.AddSurgicalSupply(supply));
    }

    public static async Task<IResult> AddCleaningSupply(
       CleaningSupply supply,
       StoreKeeper storeKeeper)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await storeKeeper.AddCleaningSupply(supply));
    }

    public static async Task<IResult> GetSupplyById(
        int id,
        StoreKeeper storeKeeper)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await storeKeeper.GetSupplyById(id));
    }

    public static async Task<IResult> RemoveSupply(
    int id,
    StoreKeeper storeKeeper)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await storeKeeper.RemoveSupply(id));
    }

    public static async Task<IResult> UpdateMedicalSupply(
        int id,
        MedicalSupply updatedSupply,
        StoreKeeper storeKeeper)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await storeKeeper.UpdateMedicalSupply(id, updatedSupply));
    }

    public static async Task<IResult> UpdateSurgicalSupply(
        int id,
        SurgicalSupply updatedSupply,
        StoreKeeper storeKeeper)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await storeKeeper.UpdateSurgicalSupply(id, updatedSupply));
    }

    public static async Task<IResult> UpdateCleaningSupply(
        int id,
        CleaningSupply updatedSupply,
        StoreKeeper storeKeeper)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await storeKeeper.UpdateCleaningSupply(id, updatedSupply));
    }
}
