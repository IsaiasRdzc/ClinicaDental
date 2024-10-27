namespace ClinicaDental.ApiService.Appointments.Services;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;

public class WorkScheduleAdmin
{
    private readonly ScheduleRegistry scheduleRegistry;
    private readonly DoctorRegistry doctorRegistry;

    public WorkScheduleAdmin(ScheduleRegistry scheduleRegistry, DoctorRegistry doctorRegistry)
    {
        this.scheduleRegistry = scheduleRegistry;
        this.doctorRegistry = doctorRegistry;
    }

    public async Task TEMP_InitializeDoctorAccount(Doctor doctor)
    {
        await this.doctorRegistry.AddDoctor(doctor);

        foreach (ClinicHours? schedule in await this.GetClinicHours())
        {
            var defaultSchedule = new DoctorDaySchedule() {
                DoctorId = doctor.Id,
                StartTime = schedule.OpeningTime,
                EndTime = schedule.ClosingTime,
                IsOff = schedule.IsClosed,
            };
            await this.scheduleRegistry.AddDoctorSchedule(defaultSchedule);
        }
    }

    public async Task<IEnumerable<ClinicHours?>> GetClinicHours()
    {
        var clinicHours = await this.scheduleRegistry.GetClinicHoursList();

        if (clinicHours is null)
        {
            throw new KeyNotFoundException("The clinic has no default schedule");
        }

        return clinicHours;
    }

    public async Task SetClinicHours(ClinicHours clinicHours)
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

    public async Task SetDoctorSchedule(DoctorDaySchedule schedule)
    {
        var doctor = await this.doctorRegistry.GetDoctorWithId(schedule.DoctorId);

        if (doctor is null)
        {
            throw new KeyNotFoundException("Doctor not found.");
        }

        if (!await this.IsInsideClinicHours(schedule))
        {
            throw new InvalidOperationException("The requested schedule is outside the available hours");
        }

        if (await this.ScheduleAlreadyExists(schedule))
        {
            await this.scheduleRegistry.UpdateDoctorSchedule(schedule);
        }
        else
        {
            await this.scheduleRegistry.AddDoctorSchedule(schedule);
        }
    }

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

    private async Task<bool> IsInsideClinicHours(DoctorDaySchedule schedule)
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
