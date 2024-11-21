namespace ClinicaDental.ApiService.DataBase.Models.Purchases;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClinicaDental.ApiService.DataBase.Models;


public class Supplier
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string PhoneNumber { get; set; } = null!;

    public List<Purchase> Purchases { get; set; } = new();

    public Supplier()
    {
    }

    public Supplier(int id)
    {
        this.Id = id;
    }
}
