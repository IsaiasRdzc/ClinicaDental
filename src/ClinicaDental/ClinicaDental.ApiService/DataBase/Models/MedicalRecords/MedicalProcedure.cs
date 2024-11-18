namespace ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MedicalProcedure
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MedicalProcedureId { get; private set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    private double ProcedureCost { get; set; }

    public int MedicalRecordId { get; private set; }
}
