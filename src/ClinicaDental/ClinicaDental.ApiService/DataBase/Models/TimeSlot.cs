namespace ClinicaDental.ApiService.DataBase.Models;

using Microsoft.EntityFrameworkCore;

[Owned]
public class TimeSlot
{
    public DayOfWeek Day { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool Contains(TimeSlot containedSlot)
    {
        bool isContained =
            this.Day == containedSlot.Day
            && this.StartTime <= containedSlot.StartTime
            && this.EndTime >= containedSlot.EndTime;

        return isContained;
    }
}
