namespace ClinicaDental.ApiService.DataBase.Registries.Purchases;

using ClinicaDental.ApiService.DataBase.Models.Purchases;
using Microsoft.EntityFrameworkCore;
public class PurchaseRegistry
    {
        private readonly AppDbContext _context;

        public PurchaseRegistry(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<List<Purchase>> GetAllPurchasesAsync()
        {
            return await this._context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.Type)
                .Include(p => p.Details)
                .ToListAsync();
        }

        // Agregar una nueva compra
        public async Task AddPurchaseAsync(Purchase purchase)
        {
            await this._context.Purchases.AddAsync(purchase);
            await this._context.SaveChangesAsync();
        }

        // Obtener una compra específica por ID
        public async Task<Purchase?> GetPurchaseByIdAsync(int id)
        {
            return await this._context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.Type)
                .Include(p => p.Details)
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task DeletePurchaseAsync(int id)
        {
            var purchase = await this._context.Purchases.FindAsync(id);
            if (purchase != null)
            {
                this._context.Purchases.Remove(purchase);
                await this._context.SaveChangesAsync();
            }
        }
}
