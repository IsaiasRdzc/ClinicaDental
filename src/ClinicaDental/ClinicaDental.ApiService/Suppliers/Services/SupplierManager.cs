﻿namespace ClinicaDental.ApiService.Suppliers.Services
{
    using ClinicaDental.ApiService.DataBase.Models.Purchases;
    using ClinicaDental.ApiService.Suppliers;
   using ClinicaDental.ApiService.DataBase.Registries.Purchases;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SupplierManager
    {
        private readonly SuppliersRegistry _suppliersRegistry;

        public SupplierManager(SuppliersRegistry suppliersRegistry)
        {
            _suppliersRegistry = suppliersRegistry;
        }

        public async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            return await _suppliersRegistry.GetAllSuppliersAsync();
        }

        public async Task<Supplier?> GetSupplierByIdAsync(int supplierId)
        {
            SupplierValidator.ValidateSupplierId(supplierId);
            return await _suppliersRegistry.GetSupplierByIdAsync(supplierId);
        }

        public async Task AddSupplierAsync(Supplier supplier)
        {
            SupplierValidator.ValidateSupplier(supplier);
            await _suppliersRegistry.AddSupplierAsync(supplier);
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            if (supplier == null || supplier.Id <= 0)
            {
                throw new ArgumentException("Proveedor inválido o no especificado");
            }

            var existingSupplier = await _suppliersRegistry.GetSupplierByIdAsync(supplier.Id);
            if (existingSupplier == null)
            {
                throw new KeyNotFoundException($"No se encontró el proveedor con ID {supplier.Id}");
            }

            // Valida campos del proveedor
            SupplierValidator.ValidateSupplier(supplier);

            // Actualiza solo propiedades necesarias
            existingSupplier.Name = supplier.Name;
            existingSupplier.PhoneNumber = supplier.PhoneNumber;

            await _suppliersRegistry.UpdateSupplierAsync(existingSupplier);
        }


        public async Task DeleteSupplierAsync(int supplierId)
        {
            SupplierValidator.ValidateSupplierId(supplierId);
            var existingSupplier = await _suppliersRegistry.GetSupplierByIdAsync(supplierId);
            if (existingSupplier == null)
            {
                throw new KeyNotFoundException($"No se encontró el proveedor con ID {supplierId}");
            }

            await _suppliersRegistry.DeleteSupplierAsync(supplierId);
        }
    }
}
