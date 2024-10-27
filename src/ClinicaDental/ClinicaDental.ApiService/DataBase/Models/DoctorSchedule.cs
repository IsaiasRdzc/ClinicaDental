namespace ClinicaDental.ApiService.DataBase.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

public class DoctorSchedule
{
    // Constructor for seeding purposes
    public DoctorSchedule(int id)
    {
        this.Id = id; // Set the ID for seeding
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    [ForeignKey("Doctor")]
    public int DoctorId { get; set; }

    [Required]
    public DayOfWeek DayOfWeek { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    public TimeOnly EndTime { get; set; }

    [Required]
    public bool IsOff { get; set; } = false;
}
