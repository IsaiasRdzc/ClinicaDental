namespace ClinicaDental.ApiService.DataBase.Models.Inventory;

using System.ComponentModel.DataAnnotations;

public class MedicalSupply : Supply
{
    public MedicalSupply(int id)
        : base(id)
    {
    }

    public DateOnly ExpirationDate { get; set; }

    public required string MedicationType { get; set; }

    public required string LotNumber { get; set; }
}
