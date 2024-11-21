namespace ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Patient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PatientId { get; private set; }

    [Required]
    public required string PatientNames { get; set; }

    [Required]
    public required string PatientSecondNames { get; set; }

    [Required]
    public required int PatientAge { get; set; }

    [Required]
    public required string PatientDirection { get; set; }

    public string? PatientPhoneNumber { get; set; }

    public string? PatientEmail { get; set; }
}
