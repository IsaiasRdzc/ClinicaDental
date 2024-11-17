namespace ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using System.ComponentModel.DataAnnotations;

public class MedicalProcedure
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }

    private double ProcedureCost { get; set; }
}
