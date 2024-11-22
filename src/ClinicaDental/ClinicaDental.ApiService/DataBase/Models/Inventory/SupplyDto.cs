namespace ClinicaDental.ApiService.DataBase.Models.Inventory;

public class SupplyDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Stock { get; set; }

    public string Type { get; set; } = null!;
}
