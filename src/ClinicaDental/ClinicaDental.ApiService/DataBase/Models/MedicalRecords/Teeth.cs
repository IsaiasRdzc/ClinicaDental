namespace ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using System.ComponentModel.DataAnnotations;

public class Teeth
{
    [Required]
    public int Name { get; init; }

    [Required]
    public string Description { get; set; }

    public bool isSuperNumerary { get; init; }
}
