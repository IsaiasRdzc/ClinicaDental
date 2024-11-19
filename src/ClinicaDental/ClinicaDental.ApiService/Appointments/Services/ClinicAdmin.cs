namespace ClinicaDental.ApiService.Appointments.Services;

using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;

using Microsoft.EntityFrameworkCore;

public class ClinicAdmin
{
    private readonly ScheduleRegistry scheduleRegistry;
    private readonly DoctorRegistry doctorRegistry;

    public ClinicAdmin(ScheduleRegistry scheduleRegistry, DoctorRegistry doctorRegistry)
    {
        this.scheduleRegistry = scheduleRegistry;
        this.doctorRegistry = doctorRegistry;
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

    public async Task CreateDoctorAccount(Doctor doctor)
    {
        await this.doctorRegistry.AddDoctor(doctor);

        foreach (ClinicDayBussinesHours? schedule in await this.GetClinicBussinesHours())
        {
            if (schedule is null)
            {
                continue;
            }

            var defaultSchedule = new DoctorDaySchedule()
            {
                DoctorId = doctor.Id,
                StartTime = schedule.OpeningTime,
                EndTime = schedule.ClosingTime,
                IsOff = schedule.IsClosed,
            };
            await this.scheduleRegistry.AddDoctorSchedule(defaultSchedule);
        }
    }

    public async Task<List<Doctor>> GetDoctorsList()
    {
        var doctors = await this.doctorRegistry.GetDoctorsList().ToListAsync();

        return doctors;
    }

    public async Task SetDoctorDaySchedule(DoctorDaySchedule daySchedule)
    {
        var doctor = await this.doctorRegistry.GetDoctorWithId(daySchedule.DoctorId);

        if (doctor is null)
        {
            throw new KeyNotFoundException("Doctor not found.");
        }

        if (!await this.IsWithinClinicHours(daySchedule))
        {
            throw new InvalidOperationException("The requested schedule is outside the available hours");
        }

        if (await this.ScheduleAlreadyExists(daySchedule))
        {
            await this.scheduleRegistry.UpdateDoctorSchedule(daySchedule);
        }
        else
        {
            await this.scheduleRegistry.AddDoctorSchedule(daySchedule);
        }
    }

    // Private functions
    private async Task<bool> ScheduleAlreadyExists(DoctorDaySchedule schedule)
    {
        var doctor = await this.doctorRegistry.GetDoctorWithId(schedule.DoctorId);

        if (doctor is null)
        {
            throw new KeyNotFoundException("Doctor not found.");
        }

        var scheduleAlreadyExists = doctor.Schedules
            .Exists(existingSchedule => existingSchedule.DayOfWeek == schedule.DayOfWeek);

        return scheduleAlreadyExists;
    }

    private async Task<bool> IsWithinClinicHours(DoctorDaySchedule schedule)
    {
        var clinicSchedule = await this.scheduleRegistry.GetClinicHoursByDay(schedule.DayOfWeek);

        if (clinicSchedule is null)
        {
            throw new KeyNotFoundException("Schedule not found.");
        }

        var isInsideClinicHours = clinicSchedule.OpeningTime <= schedule.StartTime &&
            clinicSchedule.ClosingTime >= schedule.EndTime;

        return isInsideClinicHours;
    }
}
