namespace ClinicaDental.ApiService.DataBase.Registries;

using System.Xml.Linq;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models.Supplies;
using Microsoft.EntityFrameworkCore;

public class SuppliesRegistry
{
    private readonly AppDbContext context;

    public SuppliesRegistry(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Supply>> GetSuppliesList()
    {
        var suppliesList = await this.context.Supplies.ToListAsync();
        return suppliesList;
    }

    public async Task<Supply?> GetSupplyById(int id)
    {
        var supply = await this.context.Supplies.FindAsync(id);
        return supply;
    }

    public async Task AddSupply(Supply supply)
    {
        this.context.Supplies.Add(supply);
        await this.context.SaveChangesAsync();
    }

    public async Task UpdateSupply(Supply supply)
    {
        // Marcar la entidad como modificada
        this.context.Supplies.Update(supply);

        // Guardar cambios en la base de datos
        await this.context.SaveChangesAsync();
    }

    public async Task DeleteSupply(int id)
    {
        var supply = await this.GetSupplyById(id);

        if (supply is null)
        {
            throw new KeyNotFoundException($"Supply with ID {id} not found.");
        }

        this.context.Supplies.Remove(supply);
        await this.context.SaveChangesAsync();
    }

}
