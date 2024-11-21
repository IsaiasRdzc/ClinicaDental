namespace ClinicaDental.ApiService.DataBase.Models.Appointments;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Doctor
{
    // Constructor for seeding purposes
    public Doctor(int id)
    {
        this.Id = id; // Set the ID for seeding
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public string Name { get; set; } = null!;

    public List<DoctorDaySchedule> Schedules { get; set; } = null!;
}
