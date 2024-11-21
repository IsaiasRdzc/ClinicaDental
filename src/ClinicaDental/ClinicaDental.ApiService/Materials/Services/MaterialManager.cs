namespace ClinicaDental.ApiService.Materials.Services
{
    using ClinicaDental.ApiService.DataBase.Models.Purchases;
    using ClinicaDental.ApiService.Materials;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MaterialManager
    {
        private readonly MaterialsRegistry _materialsRegistry;

        public MaterialManager(MaterialsRegistry materialsRegistry)
        {
            _materialsRegistry = materialsRegistry;
        }

        public async Task<List<Material>> GetAllMaterialsAsync()
        {
            return await _materialsRegistry.GetAllMaterialsAsync();
        }

        public async Task<Material?> GetMaterialByIdAsync(int materialId)
        {
            MaterialValidator.ValidateMaterialId(materialId);
            return await _materialsRegistry.GetMaterialByIdAsync(materialId);
        }

        public async Task AddMaterialAsync(Material material)
        {
            MaterialValidator.ValidateMaterial(material);
            await _materialsRegistry.AddMaterialAsync(material);
        }

        public async Task UpdateMaterialAsync(Material material)
        {
            MaterialValidator.ValidateMaterial(material);
            var existingMaterial = await _materialsRegistry.GetMaterialByIdAsync(material.Id);
            if (existingMaterial == null)
            {
                throw new KeyNotFoundException($"No se encontró el material con ID {material.Id}");
            }

            await _materialsRegistry.UpdateMaterialAsync(material);
        }

        public async Task DeleteMaterialAsync(int materialId)
        {
            MaterialValidator.ValidateMaterialId(materialId);
            var existingMaterial = await _materialsRegistry.GetMaterialByIdAsync(materialId);
            if (existingMaterial == null)
            {
                throw new KeyNotFoundException($"No se encontró el material con ID {materialId}");
            }

            await _materialsRegistry.DeleteMaterialAsync(materialId);
        }
    }
}
