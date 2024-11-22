using ClinicaDental.ApiService.DataBase.Models.Purchases;

namespace ClinicaDental.ApiService.Purchases.Services
{
    public static class PurchaseValidator
    {
        public static void ValidatePurchase(Purchase purchase)
        {
            if (purchase == null)
            {
                throw new ArgumentException("La compra no puede ser nula.");
            }

            if (purchase.SupplierId <= 0)
            {
                throw new ArgumentException("El ID del proveedor debe ser válido.");
            }

            if (purchase.TypeId <= 0)
            {
                throw new ArgumentException("El tipo de compra debe ser válido.");
            }

            if (purchase.Details == null || !purchase.Details.Any())
            {
                throw new ArgumentException("La compra debe contener al menos un detalle.");
            }

            foreach (var detail in purchase.Details)
            {
                if (detail.MaterialId <= 0)
                {
                    throw new ArgumentException("Cada detalle debe tener un ID de material válido.");
                }
            }
        }

        public static void ValidatePurchaseId(int purchaseId)
        {
            if (purchaseId <= 0)
            {
                throw new ArgumentException("El ID de la compra debe ser válido.");
            }
        }
    }
}
