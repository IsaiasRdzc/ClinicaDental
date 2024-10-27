namespace ClinicaDental.ApiService.DataBase.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Schedule
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public List<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();

    public bool Contains(TimeSlot containedSlot)
    {
        bool isContained =
            this.TimeSlots.Exists(timeSlot => timeSlot.Contains(containedSlot));

        return isContained;
    }
}
