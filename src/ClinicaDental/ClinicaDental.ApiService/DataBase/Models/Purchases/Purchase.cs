namespace ClinicaDental.ApiService.DataBase.Models.Purchases;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Purchase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime RequiredDate { get; set; }

    [Required]
    public int TypeId { get; set; } // Foreign key hacia PurchaseType
    public PurchaseType Type { get; set; } = null!;

    [Required]
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public List<PurchaseDetail> Details { get; set; } = new();

    public Purchase() { }

    public Purchase(int id)
    {
        this.Id = id;
    }
}
