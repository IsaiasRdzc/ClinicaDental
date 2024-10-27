namespace ClinicaDental.ApiService.DataBase.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Appointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public DateOnly Date { get; set; }

    public TimeSlot Time { get; set; } = null!;

    public string PatientName { get; set; } = null!;

    public string PatientPhone { get; set; } = null!;

    public string DoctorName { get; set; } = null!;
}
