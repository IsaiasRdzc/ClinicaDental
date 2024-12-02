namespace ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MedicalProcedure
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MedicalProcedureId { get; private set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    public double ProcedureCost { get; set; }
}
