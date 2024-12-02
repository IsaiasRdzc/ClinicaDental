namespace ClinicaDental.ApiService.Suppliers
{
    using ClinicaDental.ApiService;
    using ClinicaDental.ApiService.Suppliers.Services;
    using ClinicaDental.ApiService.DataBase.Models.Purchases;
    using ClinicaDental.ApiService.MedicalRecords.Services;
    using System.Threading.Tasks;

    public static class SuppliersEndpoints
    {
        public static void MapSuppliersEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/suppliers");

            group.MapPost("newSupplier", CreateSupplier);
            group.MapGet("getSupplierById/{supplierId}", GetSupplierById);
            group.MapGet("getAllSuppliers", GetAllSuppliers);
            group.MapPut("updateSupplier/{supplierId}", UpdateSupplier);
            group.MapDelete("deleteSupplierById/{supplierId}", DeleteSupplierById);
        }

        public static async Task<IResult> CreateSupplier(Supplier supplier, SupplierManager supplierManager)
        {
            return await ErrorOrResultHandler.HandleResult(async () => await supplierManager.AddSupplierAsync(supplier));
        }

        public static async Task<IResult> GetSupplierById(int supplierId, SupplierManager supplierManager)
        {
            return await ErrorOrResultHandler.HandleResult(async () => await supplierManager.GetSupplierByIdAsync(supplierId));
        }

        public static async Task<IResult> GetAllSuppliers(SupplierManager supplierManager)
        {
            return await ErrorOrResultHandler.HandleResult(async () => await supplierManager.GetAllSuppliersAsync());
        }

        public static async Task<IResult> UpdateSupplier(int supplierId, Supplier supplier, SupplierManager supplierManager)
        {
            return await ErrorOrResultHandler.HandleResult(async () => await supplierManager.UpdateSupplierAsync(supplier));
        }

        public static async Task<IResult> DeleteSupplierById(int supplierId, SupplierManager supplierManager)
        {
            return await ErrorOrResultHandler.HandleResult(async () => await supplierManager.DeleteSupplierAsync(supplierId));
        }
    }
}
