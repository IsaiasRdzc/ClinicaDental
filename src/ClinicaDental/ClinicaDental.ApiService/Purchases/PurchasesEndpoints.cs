using ClinicaDental.ApiService;
using ClinicaDental.ApiService.DataBase.Models.Purchases;
using ClinicaDental.ApiService.Purchases.Services;
using ClinicaDental.ApiService.Purchases.Services;

namespace ClinicaDental.ApiService.Purchases
{
    public static class PurchasesEndpoints
    {
        public static void MapPurchasesEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/purchases");

            group.MapPost("newPurchase", CreatePurchase);
            group.MapGet("getPurchaseById/{purchaseId}", GetPurchaseById);
            group.MapGet("getAllPurchases", GetAllPurchases);
            group.MapPut("updatePurchase/{purchaseId}", UpdatePurchase);
            group.MapDelete("deletePurchaseById/{purchaseId}", DeletePurchaseById);
        }

        public static async Task<IResult> CreatePurchase(Purchase purchase, PurchaseManager purchaseManager)
        {
            return await ErrorOrResultHandler.HandleResult(async () => await purchaseManager.AddPurchaseAsync(purchase));
        }

        public static async Task<IResult> GetPurchaseById(int purchaseId, PurchaseManager purchaseManager)
        {
            return await ErrorOrResultHandler.HandleResult(async () => await purchaseManager.GetPurchaseByIdAsync(purchaseId));
        }

        public static async Task<IResult> GetAllPurchases(PurchaseManager purchaseManager)
        {
            return await ErrorOrResultHandler.HandleResult(async () => await purchaseManager.GetAllPurchasesAsync());
        }

        public static async Task<IResult> UpdatePurchase(int purchaseId, Purchase purchase, PurchaseManager purchaseManager)
        {
            return await ErrorOrResultHandler.HandleResult(async () => await purchaseManager.UpdatePurchaseAsync(purchase));
        }

        public static async Task<IResult> DeletePurchaseById(int purchaseId, PurchaseManager purchaseManager)
        {
            return await ErrorOrResultHandler.HandleResult(async () => await purchaseManager.DeletePurchaseAsync(purchaseId));
        }
    }
}
