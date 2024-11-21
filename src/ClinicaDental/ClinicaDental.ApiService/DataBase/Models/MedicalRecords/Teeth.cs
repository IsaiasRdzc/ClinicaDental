namespace ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Teeth
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TeethId { get; private set; }

    [Required]
    public int Name { get; init; }

    [Required]
    public required string Description { get; set; }

    public bool IsSuperNumerary { get; init; }

    public int MedicalRecordId { get; init; }
}
