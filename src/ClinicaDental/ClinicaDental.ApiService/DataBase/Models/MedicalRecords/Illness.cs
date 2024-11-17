namespace ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using System.ComponentModel.DataAnnotations;

public class Illness
{
    [Required]
    private string? Name { get; init; }

    [Required]
    private string? Description { get; set; }

    public ICollection<Medicine>? Treatments { get; set; }
}
