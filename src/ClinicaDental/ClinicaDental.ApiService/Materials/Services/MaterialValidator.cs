namespace ClinicaDental.ApiService.Materials.Services
{
    using ClinicaDental.ApiService.DataBase.Models.Purchases;
    using System;

    public static class MaterialValidator
    {
        public static void ValidateMaterial(Material material)
        {
            if (material == null)
            {
                throw new ArgumentException("El material no puede ser nulo.");
            }

            if (string.IsNullOrWhiteSpace(material.Name))
            {
                throw new ArgumentException("El nombre del material es obligatorio.");
            }

            if (material.Name.Length > 100)
            {
                throw new ArgumentException("El nombre del material no puede superar los 100 caracteres.");
            }

            if (material.Price <= 0)
            {
                throw new ArgumentException("El precio del material debe ser mayor a 0.");
            }
        }

        public static void ValidateMaterialId(int materialId)
        {
            if (materialId <= 0)
            {
                throw new ArgumentException("El ID del material debe ser válido.");
            }
        }
    }
}
