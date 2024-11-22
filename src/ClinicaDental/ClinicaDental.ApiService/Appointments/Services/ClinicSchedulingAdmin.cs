namespace ClinicaDental.ApiService.Appointments.Services;

using ClinicaDental.ApiService.DataBase.Models.Appointments;
using ClinicaDental.ApiService.DataBase.Models.HumanResources;
using ClinicaDental.ApiService.DataBase.Registries.Appointments;
using ClinicaDental.ApiService.DataBase.Registries.HumanResources;

using Microsoft.EntityFrameworkCore;

public class ClinicSchedulingAdmin
{
    private readonly SchedulesRegistry scheduleRegistry;

    public ClinicSchedulingAdmin(SchedulesRegistry scheduleRegistry)
    {
        this.scheduleRegistry = scheduleRegistry;
    }

    public async Task<List<ClinicDayBussinesHours>> GetClinicBussinesHours()
    {
        var clinicHours = await this.scheduleRegistry.GetClinicHoursList().ToListAsync();

        if (clinicHours is null)
        {
            throw new KeyNotFoundException("The clinic has no default schedule");
        }

        return clinicHours;
    }

    public async Task SetClinicBussinesHours(ClinicDayBussinesHours clinicHours)
    {
        var existingSchedule = await this.scheduleRegistry.GetClinicHoursByDay(clinicHours.DayOfWeek);

        if (existingSchedule is null)
        {
            await this.scheduleRegistry.AddClinicHours(clinicHours);
        }
        else
        {
            await this.scheduleRegistry.UpdateClinicHours(clinicHours);
        }
    }
}
