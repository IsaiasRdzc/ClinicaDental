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
    public int PatientId { get; set; }

    public DateTime DateCreated { get; init; }

    public ICollection<Illness> Diagnosis { get; set; }

    public ICollection<Teeth> Teeths { get; set; }

    public ICollection<MedicalProcedure> MedicalProcedures { get; set; }
}
