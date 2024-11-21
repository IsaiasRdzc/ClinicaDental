namespace ClinicaDental.ApiService.DataBase.Models.Appointments;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Appointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Folio { get; private set; }

    [ForeignKey("Doctor")]
    public int DoctorId { get; set; }

    [ForeignKey("Patient")]
    public int PatientId { get; set; }

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
