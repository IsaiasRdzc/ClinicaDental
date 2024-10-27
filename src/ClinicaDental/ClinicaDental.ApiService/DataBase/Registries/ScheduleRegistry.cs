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

    public async Task<ClinicHours?> GetClinicHoursByDay(DayOfWeek dayOfWeek)
    {
        return await this.context.ClinicHours
            .FirstOrDefaultAsync(ch => ch.DayOfWeek == dayOfWeek);
    }

    public async Task UpdateClinicHours(ClinicHours clinicHours)
    {
        this.context.ClinicHours.Update(clinicHours);
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
