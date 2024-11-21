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
    public string Description { get; set; }

    public bool isSuperNumerary { get; init; }

    public int MedicalRecordId { get; private set; }
}
