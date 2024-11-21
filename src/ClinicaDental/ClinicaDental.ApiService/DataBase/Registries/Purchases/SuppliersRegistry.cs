using ClinicaDental.ApiService.DataBase.Models.Purchases;
using Microsoft.EntityFrameworkCore;

namespace ClinicaDental.ApiService.DataBase.Registries.Purchases
{
    public class SuppliersRegistry
    {
        private readonly AppDbContext _context;

        public SuppliersRegistry(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todos los proveedores
        public async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        // Agregar un nuevo proveedor
        public async Task AddSupplierAsync(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
        }

        // Buscar proveedor por ID
        public async Task<Supplier?> GetSupplierByIdAsync(int id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        // Actualizar proveedor existente
        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
        }

        // Eliminar proveedor
        public async Task DeleteSupplierAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
            }
        }
    }
}
