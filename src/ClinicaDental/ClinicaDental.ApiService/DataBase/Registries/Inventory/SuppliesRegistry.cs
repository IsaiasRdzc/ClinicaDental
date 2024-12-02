namespace ClinicaDental.ApiService.DataBase.Registries.Inventory;

using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models.Inventory;

using Microsoft.EntityFrameworkCore;

using Polly;

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

    public async Task UpdateMedicalSupplyAsync(MedicalSupply existingSupply, MedicalSupply updatedSupply)
    {
        existingSupply.Name = updatedSupply.Name;
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
        existingSupply.Stock = updatedSupply.Stock;
        existingSupply.CleaningType = updatedSupply.CleaningType;
        existingSupply.CleaningMethod = updatedSupply.CleaningMethod;
        existingSupply.CleaningDate = updatedSupply.CleaningDate;

        context.Supplies.Update(existingSupply);
        await context.SaveChangesAsync();
    }

    public IQueryable<SupplyDto> GetSupplies()
    {
        return context.Supplies
            .AsNoTracking()
            .Select(s => new SupplyDto
            {
                Id = s.Id,
                Name = s.Name,
                Stock = s.Stock,
                Type = GetSupplyType(s),
            });
    }

    public Task<Supply?> GetSupplyById(int id)
    {
        var supply = context.Supplies.FirstOrDefaultAsync(s => s.Id == id);

        return supply;
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

    public async Task<MedicalSupply?> FindExistingMedicalSupply(MedicalSupply newMedicalSupply)
    {
        return await context.MedicalSupplies.
            Where(s => s.Name == newMedicalSupply.Name &&
        s.ExpirationDate == newMedicalSupply.ExpirationDate &&
        s.MedicationType == newMedicalSupply.MedicationType).
        FirstOrDefaultAsync();
    }

    public async Task<SurgicalSupply?> FindExistingSurgicalSupply(SurgicalSupply newSurgicalSupply)
    {
        return await context.SurgicalSupplies.
            Where(s => s.Name == newSurgicalSupply.Name &&
        s.SterilizationDate == newSurgicalSupply.SterilizationDate &&
        s.SterilizationMethod == newSurgicalSupply.SterilizationMethod).
        FirstOrDefaultAsync();
    }

    public async Task<CleaningSupply?> FindExistingCleaningSupply(CleaningSupply newCleaningSupply)
    {
        return await context.CleaningSupplies.
             Where(s => s.Name == newCleaningSupply.Name &&
             s.CleaningDate == newCleaningSupply.CleaningDate &&
             s.CleaningType == newCleaningSupply.CleaningType).
            FirstOrDefaultAsync();
    }

    private static string GetSupplyType(Supply supply)
    {
        return supply switch
        {
            MedicalSupply => "Medical",
            SurgicalSupply => "Surgical",
            CleaningSupply => "Cleaning",
            _ => "Unknown",
        };
    }
}
