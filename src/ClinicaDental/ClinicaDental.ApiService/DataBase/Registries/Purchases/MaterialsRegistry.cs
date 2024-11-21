using ClinicaDental.ApiService.DataBase.Models.Purchases;
using Microsoft.EntityFrameworkCore;

namespace ClinicaDental.ApiService.DataBase.Registries.Purchases
{
    public class MaterialsRegistry
    {
        private readonly AppDbContext _context;

        public MaterialsRegistry(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los materiales
        public async Task<List<Material>> GetAllMaterialsAsync()
        {
            return await _context.Materials.ToListAsync();
        }

        // Agregar un nuevo material
        public async Task AddMaterialAsync(Material material)
        {
            await _context.Materials.AddAsync(material);
            await _context.SaveChangesAsync();
        }

        // Buscar material por ID
        public async Task<Material?> GetMaterialByIdAsync(int id)
        {
            return await _context.Materials.FindAsync(id);
        }

        // Actualizar material existente
        public async Task UpdateMaterialAsync(Material material)
        {
            _context.Materials.Update(material);
            await _context.SaveChangesAsync();
        }

        // Eliminar material
        public async Task DeleteMaterialAsync(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material != null)
            {
                _context.Materials.Remove(material);
                await _context.SaveChangesAsync();
            }
        }
    }
}
