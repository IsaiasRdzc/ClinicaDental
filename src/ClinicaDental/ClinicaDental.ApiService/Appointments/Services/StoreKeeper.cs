namespace ClinicaDental.ApiService.Appointments.Services;

using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;

using Microsoft.EntityFrameworkCore;

public class StoreKeeper
{
    private readonly SuppliesRegistry suppliesRegistry;

    public StoreKeeper(SuppliesRegistry suppliesRegistry)
    {
        this.suppliesRegistry = suppliesRegistry ?? throw new ArgumentNullException(nameof(suppliesRegistry));
    }

    public void AddSupply(Supply supply)
    {
        this.suppliesRegistry.AddSupply(supply);
    }

    public async Task<List<Supply>> GetSuppliesByType(SupplyType type)
    {
        var query = this.suppliesRegistry.GetSupplies();

        return type switch
        {
            SupplyType.Medical => await query.OfType<MedicalSupply>().ToListAsync<Supply>(),
            SupplyType.Surgical => await query.OfType<SurgicalSupply>().ToListAsync<Supply>(),
            SupplyType.Cleaning => await query.OfType<CleaningSupply>().ToListAsync<Supply>(),
            _ => await query.ToListAsync(),
        };
    }

    public async Task<List<Supply>> GetSupplies()
    {
        return await this.suppliesRegistry.GetSupplies().ToListAsync();
    }

    public void SaveChanges()
    {
        this.suppliesRegistry.SaveChanges();
    }
}
