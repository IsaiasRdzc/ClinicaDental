namespace ClinicaDental.ApiService.HumanResources;

using ClinicaDental.ApiService.DataBase.Models.Appointments;
using ClinicaDental.ApiService.DataBase.Models.HumanResources;
using ClinicaDental.ApiService.DataBase.Registries.Appointments;
using ClinicaDental.ApiService.DataBase.Registries.HumanResources;

using Microsoft.EntityFrameworkCore;

public class PersonelAdmin
{
    private readonly SchedulesRegistry scheduleRegistry;
    private readonly DoctorsRegistry doctorRegistry;

    public PersonelAdmin(SchedulesRegistry scheduleRegistry, DoctorsRegistry doctorRegistry)
    {
        this.scheduleRegistry = scheduleRegistry;
        this.doctorRegistry = doctorRegistry;
    }

    public async Task CreateDoctorAccount(Doctor doctor)
    {
        await this.doctorRegistry.AddDoctor(doctor);

        var clinicBussinesHours = await this.scheduleRegistry.GetClinicHoursList().ToListAsync();

        foreach (ClinicDayBussinesHours? schedule in clinicBussinesHours)
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
