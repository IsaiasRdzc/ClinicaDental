namespace ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MedicalRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RecordId { get; private set; }

    public DateTime DateCreated { get; init; }

    public ICollection<Illness> Diagnostic { get; set; }

    public ICollection<Teeth> Teeths { get; set; }

    public ICollection<MedicalProcedure> MedicalProcedures { get; set; }
}
