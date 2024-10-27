namespace ClinicaDental.ApiService.DataBase.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ClinicHours
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    [Required]
    public DayOfWeek DayOfWeek { get; set; }

    [Required]
    public TimeOnly OpeningTime { get; set; }

    [Required]
    public TimeOnly ClosingTime { get; set; }

    public bool IsClosed { get; set; } = false;
}
