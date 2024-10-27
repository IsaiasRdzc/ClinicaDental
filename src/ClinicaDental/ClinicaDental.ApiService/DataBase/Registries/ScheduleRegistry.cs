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

    public async Task<DoctorDaySchedule?> GetDoctorSchedule(int doctorId, DayOfWeek dayOfWeek)
    {
        var schedules = await this.GetDoctorSchedules(doctorId);

        var schedule = schedules
            .First(schedule => schedule.DayOfWeek == dayOfWeek);

        return schedule;
    }

    public async Task<IEnumerable<DoctorDaySchedule>> GetDoctorSchedules(int doctorId)
    {
        return await this.context.DoctorDaySchedules
            .Where(ds => ds.DoctorId == doctorId)
            .ToListAsync();
    }

    public async Task UpdateDoctorSchedule(DoctorDaySchedule doctorSchedule)
    {
        var existingSchedule = await this.GetDoctorSchedule(doctorSchedule.DoctorId, doctorSchedule.DayOfWeek);

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
        var doctorSchedule = await this.GetDoctorSchedule(doctorId, dayOfWeek);
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

    public async Task<IEnumerable<ScheduleModification>> GetScheduleModificationsByDate(DateOnly date)
    {
        return await this.context.ScheduleModifications
            .Where(mod => mod.Date == date)
            .ToListAsync();
    }

    public async Task<IEnumerable<ScheduleModification>> GetScheduleModificationsForDoctorOnDate(int doctorId, DateOnly date)
    {
        return await this.context.ScheduleModifications
            .Where(mod => mod.Date == date && mod.DoctorId == doctorId)
            .ToListAsync();
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
    public async Task AddClinicHours(ClinicHours clinicHours)
    {
        await this.context.ClinicHours.AddAsync(clinicHours);
        await this.context.SaveChangesAsync();
    }

    public async Task<ClinicHours?> GetClinicHours(int id)
    {
        return await this.context.ClinicHours.FindAsync(id);
    }

    public async Task<IEnumerable<ClinicHours?>> GetClinicHoursList()
    {
        return await this.context.ClinicHours.ToListAsync();
    }

    public async Task<ClinicHours?> GetClinicHoursByDay(DayOfWeek dayOfWeek)
    {
        return await this.context.ClinicHours
            .FirstOrDefaultAsync(ch => ch.DayOfWeek == dayOfWeek);
    }

    public async Task UpdateClinicHours(ClinicHours clinicHours)
    {
        var existingSchedule = await this.GetClinicHoursByDay(clinicHours.DayOfWeek);

        if (existingSchedule is null)
        {
            throw new KeyNotFoundException("Clinic schedule not found.");
        }

        existingSchedule.OpeningTime = clinicHours.OpeningTime;
        existingSchedule.ClosingTime = clinicHours.ClosingTime;
        existingSchedule.IsClosed = clinicHours.IsClosed;

        this.context.ClinicHours.Update(existingSchedule);
        await this.context.SaveChangesAsync();
    }

    public async Task DeleteClinicHours(int id)
    {
        var clinicHours = await this.GetClinicHours(id);
        if (clinicHours != null)
        {
            this.context.ClinicHours.Remove(clinicHours);
            await this.context.SaveChangesAsync();
        }
    }
}
