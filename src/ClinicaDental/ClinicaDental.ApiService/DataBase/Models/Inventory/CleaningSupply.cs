namespace ClinicaDental.ApiService.DataBase.Models.Inventory;

public class CleaningSupply : Supply
{
    public CleaningSupply(int id)
        : base(id)
    {
    }

    public required string CleaningType { get; set; }

    public required string CleaningMethod { get; set; }

    public DateOnly CleaningDate { get; set; }
}
