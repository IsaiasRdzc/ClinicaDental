namespace ClinicaDental.ApiService.DataBase.Registries;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;
using Microsoft.EntityFrameworkCore;

public class ScheduleRegistry
{
    private readonly AppDbContext context;

    public ScheduleRegistry(AppDbContext context)
    {
        this.context = context;
    }

    // Doctor schedule CRUD
    public async Task AddDoctorSchedule(DoctorDaySchedule doctorSchedule)
    {
        await this.context.DoctorDaySchedules.AddAsync(doctorSchedule);
        await this.context.SaveChangesAsync();
    }

    public DoctorDaySchedule? GetDoctorSchedule(int doctorId, DayOfWeek dayOfWeek)
    {
        var schedules = this.GetDoctorSchedules(doctorId);

        var schedule = schedules
            .Where(schedule => schedule.DayOfWeek == dayOfWeek)
            .FirstOrDefault();

        return schedule;
    }

    public IQueryable<DoctorDaySchedule> GetDoctorSchedules(int doctorId)
    {
        var doctorSchedules = this.context.DoctorDaySchedules
            .Where(ds => ds.DoctorId == doctorId)
            .AsQueryable();

        return doctorSchedules;
    }

    public async Task UpdateDoctorSchedule(DoctorDaySchedule doctorSchedule)
    {
        var existingSchedule = this.GetDoctorSchedule(doctorSchedule.DoctorId, doctorSchedule.DayOfWeek);

        if (existingSchedule is null)
        {
            throw new KeyNotFoundException("Doctor schedule not found.");
        }

        existingSchedule.StartTime = doctorSchedule.StartTime;
        existingSchedule.EndTime = doctorSchedule.EndTime;
        existingSchedule.IsOff = doctorSchedule.IsOff;

        this.context.DoctorDaySchedules.Update(existingSchedule);
        await this.context.SaveChangesAsync();
    }

    public async Task DeleteDoctorSchedule(int doctorId, DayOfWeek dayOfWeek)
    {
        var doctorSchedule = this.GetDoctorSchedule(doctorId, dayOfWeek);
        if (doctorSchedule != null)
        {
            this.context.DoctorDaySchedules.Remove(doctorSchedule);
            await this.context.SaveChangesAsync();
        }
    }

    // Schedule modifications CRUD
    public async Task AddScheduleModification(ScheduleModification modification)
    {
        await this.context.ScheduleModifications.AddAsync(modification);
        await this.context.SaveChangesAsync();
    }

    public async Task<ScheduleModification?> GetScheduleModification(int id)
    {
        return await this.context.ScheduleModifications.FindAsync(id);
    }

    public IQueryable<ScheduleModification> GetScheduleModificationsByDate(DateOnly date)
    {
        var scheduleModifications = this.context.ScheduleModifications
            .Where(mod => mod.Date == date)
            .AsQueryable();

        return scheduleModifications;
    }

    public IQueryable<ScheduleModification> GetScheduleModificationsForDoctorOnDate(int doctorId, DateOnly date)
    {
        var scheduleModifications = this.context.ScheduleModifications
            .Where(mod => mod.Date == date && mod.DoctorId == doctorId)
            .AsQueryable();

        return scheduleModifications;
    }

    public async Task UpdateScheduleModification(ScheduleModification modification)
    {
        this.context.ScheduleModifications.Update(modification);
        await this.context.SaveChangesAsync();
    }

    public async Task DeleteScheduleModification(int id)
    {
        var modification = await this.GetScheduleModification(id);
        if (modification != null)
        {
            this.context.ScheduleModifications.Remove(modification);
            await this.context.SaveChangesAsync();
        }
    }

    // Clinic hours CRUD
    public async Task AddClinicHours(ClinicDayBussinesHours clinicHours)
    {
        await this.context.ClinicDayBussinesHours.AddAsync(clinicHours);
        await this.context.SaveChangesAsync();
    }

    public async Task<ClinicDayBussinesHours?> GetClinicHours(int id)
    {
        return await this.context.ClinicDayBussinesHours.FindAsync(id);
    }

    public IQueryable<ClinicDayBussinesHours> GetClinicHoursList()
    {
        return this.context.ClinicDayBussinesHours.AsQueryable();
    }

    public async Task<ClinicDayBussinesHours?> GetClinicHoursByDay(DayOfWeek dayOfWeek)
    {
        return await this.context.ClinicDayBussinesHours
            .FirstOrDefaultAsync(ch => ch.DayOfWeek == dayOfWeek);
    }

    public async Task UpdateClinicHours(ClinicDayBussinesHours clinicHours)
    {
        var existingClinicHours = await this.GetClinicHoursByDay(clinicHours.DayOfWeek);

        if (existingClinicHours is null)
        {
            throw new KeyNotFoundException("Clinic schedule not found.");
        }

        existingClinicHours.OpeningTime = clinicHours.OpeningTime;
        existingClinicHours.ClosingTime = clinicHours.ClosingTime;
        existingClinicHours.IsClosed = clinicHours.IsClosed;

        this.context.ClinicDayBussinesHours.Update(existingClinicHours);
        await this.context.SaveChangesAsync();
    }

    public async Task DeleteClinicHours(int id)
    {
        var clinicDayBussinesHours = await this.GetClinicHours(id);
        if (clinicDayBussinesHours != null)
        {
            this.context.ClinicDayBussinesHours.Remove(clinicDayBussinesHours);
            await this.context.SaveChangesAsync();
        }
    }
}
