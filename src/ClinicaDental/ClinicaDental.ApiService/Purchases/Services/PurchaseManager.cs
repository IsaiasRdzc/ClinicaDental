using ClinicaDental.ApiService.DataBase.Models.Purchases;
using ClinicaDental.ApiService.Purchases.Services;
using ClinicaDental.ApiService.DataBase.Registries.Purchases;

namespace ClinicaDental.ApiService.Purchases.Services
{
    public class PurchaseManager
    {
        private readonly PurchasesRegistry _purchasesRegistry;

        public PurchaseManager(PurchasesRegistry purchasesRegistry)
        {
            _purchasesRegistry = purchasesRegistry;
        }

        public async Task<List<Purchase>> GetAllPurchasesAsync()
        {
            return await _purchasesRegistry.GetAllPurchasesAsync();
        }

        public async Task<Purchase?> GetPurchaseByIdAsync(int id)
        {
            PurchaseValidator.ValidatePurchaseId(id); // Validar ID
            return await _purchasesRegistry.GetPurchaseByIdAsync(id);
        }

        public async Task AddPurchaseAsync(Purchase purchase)
        {
            PurchaseValidator.ValidatePurchase(purchase); // Validar la compra
            await _purchasesRegistry.AddPurchaseAsync(purchase);
        }

        public async Task UpdatePurchaseAsync(Purchase purchase)
        {
            PurchaseValidator.ValidatePurchase(purchase); // Validar la compra
            var existingPurchase = await _purchasesRegistry.GetPurchaseByIdAsync(purchase.Id);
            if (existingPurchase == null)
            {
                throw new KeyNotFoundException($"No se encontró la compra con ID {purchase.Id}");
            }

            await _purchasesRegistry.UpdatePurchaseAsync(purchase);
        }

        public async Task DeletePurchaseAsync(int id)
        {
            PurchaseValidator.ValidatePurchaseId(id); // Validar ID
            var existingPurchase = await _purchasesRegistry.GetPurchaseByIdAsync(id);
            if (existingPurchase == null)
            {
                throw new KeyNotFoundException($"No se encontró la compra con ID {id}");
            }

            await _purchasesRegistry.DeletePurchaseAsync(id);
        }
    }

    public class PurchasesRegistry
    {
    }
}
