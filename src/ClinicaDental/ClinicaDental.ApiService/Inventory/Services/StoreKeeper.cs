namespace ClinicaDental.ApiService.Inventory.Services;

using ClinicaDental.ApiService.DataBase.Models.Inventory;
using ClinicaDental.ApiService.DataBase.Registries.Inventory;

using Microsoft.EntityFrameworkCore;

using Polly;

public class StoreKeeper(SuppliesRegistry suppliesRegistry)
{
    public async Task AddMedicalSupply(MedicalSupply newMedicalSupply)
    {
        if (newMedicalSupply == null)
        {
            throw new ArgumentException("El insumo médico no puede ser nulo.");
        }

        if (string.IsNullOrWhiteSpace(newMedicalSupply.MedicationType) || string.IsNullOrWhiteSpace(newMedicalSupply.LotNumber))
        {
            throw new ArgumentException("El tipo de medicamento y el número de lote son obligatorios.");
        }

        if (newMedicalSupply.Stock < 0)
        {
            throw new ArgumentException("El stock debe ser positivo");
        }

        var existingSupply = await suppliesRegistry.FindExistingMedicalSupply(newMedicalSupply);

        if (existingSupply is not null)
        {
            throw new InvalidOperationException(
                $"El suministro con los datos que proporcionaste ya existe. " +
                $"Considere actualizar el stock en lugar de agregar un nuevo suministro.");
        }

        await suppliesRegistry.AddSupply(newMedicalSupply);
    }

    public async Task AddSurgicalSupply(SurgicalSupply newSurgicalSupply)
    {
        if (newSurgicalSupply == null)
        {
            throw new ArgumentException("El insumo quirúrgico no puede ser nulo");
        }

        if (string.IsNullOrWhiteSpace(newSurgicalSupply.SurgicalType) || string.IsNullOrWhiteSpace(newSurgicalSupply.SterilizationMethod))
        {
            throw new ArgumentException("El tipo de cirugía y el método de esterilización son obligatorios.");
        }

        if (newSurgicalSupply.Stock < 0)
        {
            throw new ArgumentException("El stock debe ser positivo");
        }

        var existingSupply = await suppliesRegistry.FindExistingSurgicalSupply(newSurgicalSupply);

        if (existingSupply is not null)
        {
            throw new InvalidOperationException(
                $"Un suministro con los datos que proporcionaste ya existe. " +
                $"Considere actualizar el stock en lugar de agregar un nuevo suministro.");
        }

        await suppliesRegistry.AddSupply(newSurgicalSupply);
    }

    public async Task AddCleaningSupply(CleaningSupply newCleaningSupply)
    {
        if (newCleaningSupply == null)
        {
            throw new ArgumentException("El insumo clínico no puede ser nulo");
        }

        if (string.IsNullOrWhiteSpace(newCleaningSupply.CleaningType) || string.IsNullOrWhiteSpace(newCleaningSupply.CleaningMethod))
        {
            throw new ArgumentException("El tipo de limpieza y el método de limpieza son obligatorios.");
        }

        if (newCleaningSupply.Stock < 0)
        {
            throw new ArgumentException("El stock debe ser positivo");
        }

        var existingSupply = await suppliesRegistry.FindExistingCleaningSupply(newCleaningSupply);

        if (existingSupply is not null)
        {
            throw new InvalidOperationException(
                $"Un suministro con los datos que proporcionaste ya existe. " +
                $"Considere actualizar el stock en lugar de agregar un nuevo suministro.");
        }

        await suppliesRegistry.AddSupply(newCleaningSupply);
    }

    public async Task<Supply> GetSupplyById(int supplyIdentifier)
    {
        var supply = await suppliesRegistry.GetSupplyById(supplyIdentifier);

        if (supply is null)
        {
            throw new KeyNotFoundException($"El suministro con ID '{supplyIdentifier}' no existe.");
        }

        return supply;
    }

    public async Task<List<Supply>> GetSuppliesByType(SupplyType supplyType)
    {
        if (suppliesRegistry == null)
        {
            throw new InvalidOperationException("El repositorio de suministros no está configurado correctamente.");
        }

        if (!Enum.IsDefined(typeof(SupplyType), supplyType))
        {
            throw new ArgumentException($"El tipo de suministro '{supplyType}' no es válido.", nameof(supplyType));
        }

        var supplies = await suppliesRegistry.GetSuppliesByType(supplyType).ToListAsync();

        return supplies;
    }

    public async Task<List<SupplyDto>> GetSupplies()
    {
        return await suppliesRegistry.GetSupplies().ToListAsync();
    }

    public async Task RemoveSupply(int supplyIdentifier)
    {
        var existingSupply = await suppliesRegistry.GetSupplyById(supplyIdentifier);

        if (existingSupply is null)
        {
            throw new KeyNotFoundException($"El suministro con ID '{supplyIdentifier}' no existe.");
        }

        await suppliesRegistry.RemoveSupply(existingSupply);
    }

    public async Task UpdateMedicalSupply(
        int id,
        MedicalSupply medicalSupplyToUpdate)
    {
        var existingSupply = await suppliesRegistry.GetMedicalSupplyByIdAsync(id);

        if (existingSupply is null)
        {
            throw new KeyNotFoundException($"El suministro con ID '{id}' no existe.");
        }

        if (medicalSupplyToUpdate.Stock < 0)
        {
            throw new ArgumentException("El stock debe ser positivo");
        }

        await suppliesRegistry.UpdateMedicalSupplyAsync(existingSupply, medicalSupplyToUpdate);
    }

    public async Task UpdateSurgicalSupply(
        int id,
        SurgicalSupply surgicalSupplyToUpdate)
    {
        var existingSupply = await suppliesRegistry.GetSurgicalSupplyByIdAsync(id);

        if (existingSupply is null)
        {
            throw new KeyNotFoundException($"El suministro con ID '{id}' no existe.");
        }

        if (surgicalSupplyToUpdate.Stock < 0)
        {
            throw new ArgumentException("El stock debe ser positivo");
        }

        await suppliesRegistry.UpdateSurgicalSupplyAsync(existingSupply, surgicalSupplyToUpdate);
    }

    public async Task UpdateCleaningSupply(
        int id,
        CleaningSupply cleaningSupplyToUpdate)
    {
        var existingSupply = await suppliesRegistry.GetCleaningSupplyByIdAsync(id);

        if (existingSupply is null)
        {
            throw new KeyNotFoundException($"El suministro con ID '{cleaningSupplyToUpdate.Id}' no existe.");
        }

        if (cleaningSupplyToUpdate.Stock < 0)
        {
            throw new ArgumentException("El stock debe ser positivo");
        }

        await suppliesRegistry.UpdateCleaningSupplyAsync(existingSupply, cleaningSupplyToUpdate);
    }
}
