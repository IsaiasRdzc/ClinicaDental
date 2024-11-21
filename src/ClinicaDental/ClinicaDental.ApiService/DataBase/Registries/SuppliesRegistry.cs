namespace ClinicaDental.ApiService.DataBase.Registries;

using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;

using Microsoft.EntityFrameworkCore;

public class SuppliesRegistry(AppDbContext context)
{
    public async Task AddSupply(Supply supply)
    {
        context.Supplies.Add(supply);
        await context.SaveChangesAsync();
    }

    public async Task RemoveSupply(Supply supply)
    {
        context.Supplies.Remove(supply);
        await context.SaveChangesAsync();
    }

    public async Task UpdateSupplyAsync(Supply supply)
    {
        context.Supplies.Update(supply);
        await context.SaveChangesAsync();
    }

    public async Task UpdateMedicalSupplyAsync(MedicalSupply existingSupply, MedicalSupply updatedSupply)
    {
        existingSupply.Name = updatedSupply.Name;
        existingSupply.Description = updatedSupply.Description;
        existingSupply.Stock = updatedSupply.Stock;
        existingSupply.MedicationType = updatedSupply.MedicationType;
        existingSupply.ExpirationDate = updatedSupply.ExpirationDate;
        existingSupply.LotNumber = updatedSupply.LotNumber;

        context.Supplies.Update(existingSupply);
        await context.SaveChangesAsync();
    }

    public async Task UpdateSurgicalSupplyAsync(SurgicalSupply existingSupply, SurgicalSupply updatedSupply)
    {
        existingSupply.Name = updatedSupply.Name;
        existingSupply.Description = updatedSupply.Description;
        existingSupply.Stock = updatedSupply.Stock;
        existingSupply.SurgicalType = updatedSupply.SurgicalType;
        existingSupply.SterilizationMethod = updatedSupply.SterilizationMethod;
        existingSupply.SterilizationDate = updatedSupply.SterilizationDate;

        context.Supplies.Update(existingSupply);
        await context.SaveChangesAsync();
    }

    public async Task UpdateCleaningSupplyAsync(CleaningSupply existingSupply, CleaningSupply updatedSupply)
    {
        existingSupply.Name = updatedSupply.Name;
        existingSupply.Description = updatedSupply.Description;
        existingSupply.Stock = updatedSupply.Stock;
        existingSupply.CleaningType = updatedSupply.CleaningType;
        existingSupply.CleaningMethod = updatedSupply.CleaningMethod;
        existingSupply.CleaningDate = updatedSupply.CleaningDate;

        context.Supplies.Update(existingSupply);
        await context.SaveChangesAsync();
    }

    public IQueryable<Supply> GetSupplies()
    {
        return context.Supplies.AsQueryable().AsNoTracking();
    }

    public Supply? GetSupplyById(int id)
    {
        return context.Supplies.FirstOrDefault(s => s.Id == id);
    }

    public async Task<MedicalSupply?> GetMedicalSupplyByIdAsync(int id)
    {
        return await context.Supplies
            .OfType<MedicalSupply>()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<SurgicalSupply?> GetSurgicalSupplyByIdAsync(int id)
    {
        return await context.Supplies
            .OfType<SurgicalSupply>()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<CleaningSupply?> GetCleaningSupplyByIdAsync(int id)
    {
        return await context.Supplies
            .OfType<CleaningSupply>()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public IQueryable<Supply> GetSuppliesByType(SupplyType type)
    {
        return type switch
        {
            SupplyType.Medical => context.Supplies.OfType<MedicalSupply>(),
            SupplyType.Surgical => context.Supplies.OfType<SurgicalSupply>(),
            SupplyType.Cleaning => context.Supplies.OfType<CleaningSupply>(),
            _ => throw new InvalidOperationException("Unknown supply type."),
        };
    }
}
