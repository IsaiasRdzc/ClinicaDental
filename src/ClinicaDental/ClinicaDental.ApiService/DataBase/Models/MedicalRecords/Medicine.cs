namespace ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using System.ComponentModel.DataAnnotations;

public class Medicine
{
    [Required]
    public string? Description { get; init; }

    [Required]
    public string? Dose { get; init; }
}
