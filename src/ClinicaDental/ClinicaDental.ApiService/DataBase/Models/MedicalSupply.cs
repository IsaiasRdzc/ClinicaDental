namespace ClinicaDental.ApiService.DataBase.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MedicalSupply : Supply
{
    public string PrescriptionRequired { get; set; } = null!;
}
