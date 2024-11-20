namespace ClinicaDental.ApiService.DataBase.Registries;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;
using Microsoft.EntityFrameworkCore;

public class ScheduleRegistry(AppDbContext context)
{
    // Doctor schedule CRUD
    public async Task AddDoctorSchedule(DoctorDaySchedule doctorSchedule)
    {
        await context.DoctorDaySchedules.AddAsync(doctorSchedule);
        await context.SaveChangesAsync();
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
        var doctorSchedules = context.DoctorDaySchedules
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

        context.DoctorDaySchedules.Update(existingSchedule);
        await context.SaveChangesAsync();
    }

    public async Task DeleteDoctorSchedule(int doctorId, DayOfWeek dayOfWeek)
    {
        var doctorSchedule = this.GetDoctorSchedule(doctorId, dayOfWeek);
        if (doctorSchedule != null)
        {
            context.DoctorDaySchedules.Remove(doctorSchedule);
            await context.SaveChangesAsync();
        }
    }

    // Schedule modifications CRUD
    public async Task AddScheduleModification(ScheduleModification modification)
    {
        await context.ScheduleModifications.AddAsync(modification);
        await context.SaveChangesAsync();
    }

    public async Task<ScheduleModification?> GetScheduleModification(int id)
    {
        return await context.ScheduleModifications.FindAsync(id);
    }

    public IQueryable<ScheduleModification> GetScheduleModificationsByDate(DateOnly date)
    {
        var scheduleModifications = context.ScheduleModifications
            .Where(mod => mod.Date == date)
            .AsQueryable();

        return scheduleModifications;
    }

    public IQueryable<ScheduleModification> GetScheduleModificationsForDoctorOnDate(int doctorId, DateOnly date)
    {
        var scheduleModifications = context.ScheduleModifications
            .Where(mod => mod.Date == date && mod.DoctorId == doctorId)
            .AsQueryable();

        return scheduleModifications;
    }

    public async Task UpdateScheduleModification(ScheduleModification modification)
    {
        context.ScheduleModifications.Update(modification);
        await context.SaveChangesAsync();
    }

    public async Task DeleteScheduleModification(int id)
    {
        var modification = await this.GetScheduleModification(id);
        if (modification != null)
        {
            context.ScheduleModifications.Remove(modification);
            await context.SaveChangesAsync();
        }
    }

    // Clinic hours CRUD
    public async Task AddClinicHours(ClinicDayBussinesHours clinicHours)
    {
        await context.ClinicDayBussinesHours.AddAsync(clinicHours);
        await context.SaveChangesAsync();
    }

    public async Task<ClinicDayBussinesHours?> GetClinicHours(int id)
    {
        return await context.ClinicDayBussinesHours.FindAsync(id);
    }

    public IQueryable<ClinicDayBussinesHours> GetClinicHoursList()
    {
        return context.ClinicDayBussinesHours.AsQueryable();
    }

    public async Task<ClinicDayBussinesHours?> GetClinicHoursByDay(DayOfWeek dayOfWeek)
    {
        return await context.ClinicDayBussinesHours
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

        context.ClinicDayBussinesHours.Update(existingClinicHours);
        await context.SaveChangesAsync();
    }

    public async Task DeleteClinicHours(int id)
    {
        var clinicDayBussinesHours = await this.GetClinicHours(id);
        if (clinicDayBussinesHours != null)
        {
            context.ClinicDayBussinesHours.Remove(clinicDayBussinesHours);
            await context.SaveChangesAsync();
        }
    }
}
