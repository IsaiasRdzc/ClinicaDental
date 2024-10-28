namespace ClinicaDental.ApiService.DataBase.Models.Supplies;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Supply
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Stock { get; set; }
}
