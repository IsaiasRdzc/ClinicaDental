namespace ClinicaDental.ApiService.DataBase.Models.Inventory;
public class SurgicalSupply : Supply
{
    public required string SurgicalType { get; set; }

    public required string SterilizationMethod { get; set; }

    public DateTime SterilizationDate { get; set; }
}
