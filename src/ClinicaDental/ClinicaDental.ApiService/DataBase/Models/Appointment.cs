namespace ClinicaDental.ApiService.DataBase.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Appointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    [ForeignKey("Doctor")]
    public int DoctorId { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    public int Duration { get; set; }

    [Required]
    public string PatientName { get; set; } = null!;

    [Required]
    public string PatientPhone { get; set; } = null!;

    public TimeOnly EndTime()
    {
        return this.StartTime.AddHours(this.Duration);
    }
}
