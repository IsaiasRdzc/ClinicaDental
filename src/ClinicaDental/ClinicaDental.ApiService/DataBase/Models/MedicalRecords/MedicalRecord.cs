namespace ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MedicalRecord
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MedicalRecordId { get; private set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public int PatientId { get; init; }

    public DateTime DateCreated { get; init; }

    public ICollection<Illness> Diagnosis { get; set; } = new List<Illness>();

    public ICollection<Teeth> Teeths { get; set; } = new List<Teeth>();

    public ICollection<MedicalProcedure> MedicalProcedures { get; set; } = new List<MedicalProcedure>();
}
