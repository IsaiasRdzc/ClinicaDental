namespace ClinicaDental.ApiService.Materials
{
    using ClinicaDental.ApiService;
    using ClinicaDental.ApiService.Materials.Services;
    using ClinicaDental.ApiService.DataBase.Models.Purchases;
    using System.Threading.Tasks;

    public static class MaterialsEndpoints
    {
        public static void MapMaterialsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/materials");

            group.MapPost("newMaterial", CreateMaterial);
            group.MapGet("getMaterialById/{materialId}", GetMaterialById);
            group.MapGet("getAllMaterials", GetAllMaterials);
            group.MapPut("updateMaterial/{materialId}", UpdateMaterial);
            group.MapDelete("deleteMaterialById/{materialId}", DeleteMaterialById);
        }

        public static async Task<IResult> CreateMaterial(Material material, MaterialManager materialManager)
        {
            return await ErrorOrResultHandler.HandleResult(async () => await materialManager.AddMaterialAsync(material));
        }

        public static async Task<IResult> GetMaterialById(int materialId, MaterialManager materialManager)
        {
            return await ErrorOrResultHandler.HandleResult(async () => await materialManager.GetMaterialByIdAsync(materialId));
        }

        public static async Task<IResult> GetAllMaterials(MaterialManager materialManager)
        {
            return await ErrorOrResultHandler.HandleResult(async () => await materialManager.GetAllMaterialsAsync());
        }

        public static async Task<IResult> UpdateMaterial(int materialId, Material material, MaterialManager materialManager)
        {
            return await ErrorOrResultHandler.HandleResult(async () => await materialManager.UpdateMaterialAsync(material));
        }

        public static async Task<IResult> DeleteMaterialById(int materialId, MaterialManager materialManager)
        {
            return await ErrorOrResultHandler.HandleResult(async () => await materialManager.DeleteMaterialAsync(materialId));
        }
    }
}
