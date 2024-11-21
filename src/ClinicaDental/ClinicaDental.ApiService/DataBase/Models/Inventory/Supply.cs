namespace ClinicaDental.ApiService.DataBase.Models.Inventory;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[JsonDerivedType(typeof(MedicalSupply))]
[JsonDerivedType(typeof(SurgicalSupply))]
[JsonDerivedType(typeof(CleaningSupply))]
public class Supply
{
    public Supply(int id)
    {
        this.Id = id;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
    public int Stock { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SupplyType
{
    Medical,
    Surgical,
    Cleaning,
}
