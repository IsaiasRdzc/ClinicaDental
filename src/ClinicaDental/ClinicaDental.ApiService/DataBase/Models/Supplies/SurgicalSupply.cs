namespace ClinicaDental.ApiService.DataBase.Models.Supplies;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SurgicalSupply : Supply
{
    public required string SurgicalType { get; set; }

    public required string SterilizationMethod { get; set; }

    public DateTime SterilizationDate { get; set; }
}
