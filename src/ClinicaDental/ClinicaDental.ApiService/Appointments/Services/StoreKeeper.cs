namespace ClinicaDental.ApiService.Appointments.Services;

using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;

using Microsoft.EntityFrameworkCore;

using Polly;

public class StoreKeeper(SuppliesRegistry suppliesRegistry)
{
    public async Task AddMedicalSupply(MedicalSupply newMedicalSupply)
    {
        if (newMedicalSupply == null)
        {
            throw new ArgumentNullException(nameof(newMedicalSupply));
        }

        if (string.IsNullOrWhiteSpace(newMedicalSupply.MedicationType) || string.IsNullOrWhiteSpace(newMedicalSupply.LotNumber))
        {
            throw new ArgumentException("El tipo de medicamento y el número de lote son obligatorios.");
        }

        await suppliesRegistry.AddSupply(newMedicalSupply);
    }

    public async Task AddSurgicalSupply(SurgicalSupply newSurgicalSupply)
    {
        if (newSurgicalSupply == null)
        {
            throw new ArgumentNullException(nameof(newSurgicalSupply));
        }

        if (string.IsNullOrWhiteSpace(newSurgicalSupply.SurgicalType) || string.IsNullOrWhiteSpace(newSurgicalSupply.SterilizationMethod))
        {
            throw new ArgumentException("El tipo de cirugía y el método de esterilización son obligatorios.");
        }

        await suppliesRegistry.AddSupply(newSurgicalSupply);
    }

    public async Task AddCleaningSupply(CleaningSupply newCleaningSupply)
    {
        if (newCleaningSupply == null)
        {
            throw new ArgumentNullException(nameof(newCleaningSupply));
        }

        if (string.IsNullOrWhiteSpace(newCleaningSupply.CleaningType) || string.IsNullOrWhiteSpace(newCleaningSupply.CleaningMethod))
        {
            throw new ArgumentException("El tipo de limpieza y el método de limpieza son obligatorios.");
        }

        await suppliesRegistry.AddSupply(newCleaningSupply);
    }

    public Supply? GetSupplyById(int supplyIdentifier)
    {
        var supply = suppliesRegistry.GetSupplyById(supplyIdentifier);

        if (supply is null)
        {
            throw new KeyNotFoundException($"El suministro con ID '{supplyIdentifier}' no existe.");
        }

        return supply;
    }

    public async Task<List<Supply>> GetSuppliesByType(SupplyType supplyType)
    {
        if (!Enum.IsDefined(typeof(SupplyType), supplyType))
        {
            throw new ArgumentException($"El tipo de suministro '{supplyType}' no es válido.", nameof(supplyType));
        }

        if (suppliesRegistry == null)
        {
            throw new InvalidOperationException("El repositorio de suministros no está configurado correctamente.");
        }

        var supplies = await suppliesRegistry.GetSuppliesByType(supplyType).ToListAsync();

        return supplies;
    }

    public async Task<List<Supply>> GetSupplies()
    {
        return await suppliesRegistry.GetSupplies().ToListAsync();
    }

    public async Task RemoveSupply(int supplyIdentifier)
    {
        var existingSupply = suppliesRegistry.GetSupplyById(supplyIdentifier);

        if (existingSupply is null)
        {
            throw new KeyNotFoundException($"El suministro con ID '{supplyIdentifier}' no existe.");
        }

        await suppliesRegistry.RemoveSupply(existingSupply);
    }

    public async Task UpdateMedicalSupply(MedicalSupply medicalSupplyToUpdate)
    {
        var existingSupply = await suppliesRegistry.GetMedicalSupplyByIdAsync(medicalSupplyToUpdate.Id);

        if (existingSupply is null)
        {
            throw new KeyNotFoundException($"El suministro con ID '{medicalSupplyToUpdate.Id}' no existe.");
        }

        await suppliesRegistry.UpdateMedicalSupplyAsync(existingSupply, medicalSupplyToUpdate);
    }

    public async Task UpdateSurgicalSupply(SurgicalSupply surgicalSupplyToUpdate)
    {
        var existingSupply = await suppliesRegistry.GetSurgicalSupplyByIdAsync(surgicalSupplyToUpdate.Id);

        if (existingSupply is null)
        {
            throw new KeyNotFoundException($"El suministro con ID '{surgicalSupplyToUpdate.Id}' no existe.");
        }

        await suppliesRegistry.UpdateSurgicalSupplyAsync(existingSupply, surgicalSupplyToUpdate);
    }

    public async Task UpdateCleaningSupply(CleaningSupply cleaningSupplyToUpdate)
    {
        var existingSupply = await suppliesRegistry.GetCleaningSupplyByIdAsync(cleaningSupplyToUpdate.Id);

        if (existingSupply is null)
        {
            throw new KeyNotFoundException($"El suministro con ID '{cleaningSupplyToUpdate.Id}' no existe.");
        }

        await suppliesRegistry.UpdateCleaningSupplyAsync(existingSupply, cleaningSupplyToUpdate);
    }
}
