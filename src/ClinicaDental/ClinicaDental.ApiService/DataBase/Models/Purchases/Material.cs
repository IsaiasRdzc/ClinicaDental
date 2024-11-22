namespace ClinicaDental.ApiService.DataBase.Models.Purchases
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Material
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }
    }
}
