namespace ClinicaDental.ApiService.DataBase.Registries.Purchases;

using ClinicaDental.ApiService.DataBase.Models.Purchases;
using Microsoft.EntityFrameworkCore;

public class PurchasesDataRegistry
{
    private readonly AppDbContext context;

    public PurchasesDataRegistry(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<List<Purchase>> GetAllPurchasesAsync()
    {
        return await this.context.Purchases
            .Include(p => p.Supplier)
            .Include(p => p.Type)
            .Include(p => p.Details)
            .ToListAsync();
    }

    // Agregar una nueva compra
    public async Task AddPurchaseAsync(Purchase purchase)
    {
        await this.context.Purchases.AddAsync(purchase);
        await this.context.SaveChangesAsync();
    }

    // Obtener una compra específica por ID
    public async Task<Purchase?> GetPurchaseByIdAsync(int id)
    {
        return await this.context.Purchases
            .Include(p => p.Supplier)
            .Include(p => p.Type)
            .Include(p => p.Details)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task DeletePurchaseAsync(int id)
    {
        var purchase = await this.context.Purchases.FindAsync(id);
        if (purchase != null)
        {
            this.context.Purchases.Remove(purchase);
            await this.context.SaveChangesAsync();
        }
    }

    public async Task UpdatePurchaseAsync(Purchase updatedPurchase)
    {
        var existingPurchase = await this.context.Purchases
            .Include(p => p.Details)
            .FirstOrDefaultAsync(p => p.Id == updatedPurchase.Id);

        if (existingPurchase == null)
        {
            throw new KeyNotFoundException($"No se encontró la compra con ID {updatedPurchase.Id}");
        }

        // Actualiza los valores de la compra existente con los del objeto actualizado
        context.Entry(existingPurchase).CurrentValues.SetValues(updatedPurchase);

        // Actualiza los detalles de la compra
        foreach (var detail in updatedPurchase.Details)
        {
            var existingDetail = existingPurchase.Details.FirstOrDefault(d => d.Id == detail.Id);

            if (existingDetail != null)
            {
                // Si el detalle existe, actualiza los valores
                context.Entry(existingDetail).CurrentValues.SetValues(detail);
            }
            else
            {
                // Si el detalle no existe, añádelo
                existingPurchase.Details.Add(detail);
            }
        }

        // Elimina los detalles que ya no están en la lista actualizada
        foreach (var detail in existingPurchase.Details.ToList())
        {
            if (!updatedPurchase.Details.Any(d => d.Id == detail.Id))
            {
                context.PurchaseDetails.Remove(detail);
            }
        }

        await context.SaveChangesAsync();
    }
}
