namespace ClinicaDental.ApiService.DataBase.Models.Purchases
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class PurchaseDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PurchaseId { get; set; } // Clave foránea

        [Required]
        public int MaterialId { get; set; }
        public Material Material { get; set; } = null!;

        [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal UnitCost { get; set; }

    public PurchaseDetail()
    {
    }

    public PurchaseDetail(int id)
    {
        this.Id = id;
    }
}
}
