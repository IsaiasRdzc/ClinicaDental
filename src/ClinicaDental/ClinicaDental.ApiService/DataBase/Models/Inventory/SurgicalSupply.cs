namespace ClinicaDental.ApiService.DataBase.Models.Inventory;

public class SurgicalSupply : Supply
{
    public SurgicalSupply(int id)
        : base(id)
    {
    }

    public required string SurgicalType { get; set; }

    public required string SterilizationMethod { get; set; }

    public DateOnly SterilizationDate { get; set; }
}
