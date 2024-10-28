namespace ClinicaDental.ApiService.DataBase.Models.Supplies;

using System.ComponentModel.DataAnnotations;

public class MedicalSupply : Supply
{
    public DateTime ExpirationDate { get; set; }

    public required string MedicationType { get; set; }

    public required string LotNumber { get; set; }
}
