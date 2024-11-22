namespace ClinicaDental.ApiService.Suppliers.Services
{
    using ClinicaDental.ApiService.DataBase.Models.Purchases;
    using System;

    public static class SupplierValidator
    {
        public static void ValidateSupplier(Supplier supplier)
        {
            if (supplier == null)
            {
                throw new ArgumentException("El proveedor no puede ser nulo.");
            }

            if (string.IsNullOrWhiteSpace(supplier.Name))
            {
                throw new ArgumentException("El nombre del proveedor es obligatorio.");
            }

            if (supplier.Name.Length > 100)
            {
                throw new ArgumentException("El nombre del proveedor no puede superar los 100 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(supplier.PhoneNumber) && supplier.PhoneNumber.Length > 15)
            {
                throw new ArgumentException("El número de teléfono no puede superar los 15 caracteres.");
            }
        }

        public static void ValidateSupplierId(int supplierId)
        {
            if (supplierId <= 0)
            {
                throw new ArgumentException("El ID del proveedor debe ser válido.");
            }
        }
    }
}
