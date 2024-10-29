namespace ClinicaDental.ApiService.DataBase.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Appointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; }

    [ForeignKey("Doctor")]
    public int DoctorId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get => this.CalculateEndTime(); }

    public int DurationInHours { get; set; }

    public string PatientName { get; set; } = null!;

    public string PatientPhone { get; set; } = null!;

    private TimeOnly CalculateEndTime()
    {
        return this.StartTime.AddHours(this.DurationInHours);
    }
}
