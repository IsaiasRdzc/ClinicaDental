namespace ClinicaDental.ApiService.DataBase.Registries;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models.Appointments;

using Microsoft.EntityFrameworkCore;

public class SchedulesRegistry(AppDbContext clinicDataBase)
{
    // Doctor schedule CRUD
    public async Task AddDoctorSchedule(DoctorDaySchedule doctorSchedule)
    {
        await clinicDataBase.DoctorDaySchedulesTable.AddAsync(doctorSchedule);
        await clinicDataBase.SaveChangesAsync();
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
        var doctorSchedules = clinicDataBase.DoctorDaySchedulesTable
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

        clinicDataBase.DoctorDaySchedulesTable.Update(existingSchedule);
        await clinicDataBase.SaveChangesAsync();
    }

    public async Task DeleteDoctorSchedule(int doctorId, DayOfWeek dayOfWeek)
    {
        var doctorSchedule = this.GetDoctorSchedule(doctorId, dayOfWeek);
        if (doctorSchedule != null)
        {
            clinicDataBase.DoctorDaySchedulesTable.Remove(doctorSchedule);
            await clinicDataBase.SaveChangesAsync();
        }
    }

    // Schedule modifications CRUD
    public async Task AddScheduleModification(ScheduleModification modification)
    {
        await clinicDataBase.ScheduleModificationsTable.AddAsync(modification);
        await clinicDataBase.SaveChangesAsync();
    }

    public async Task<ScheduleModification?> GetScheduleModification(int id)
    {
        return await clinicDataBase.ScheduleModificationsTable.FindAsync(id);
    }

    public IQueryable<ScheduleModification> GetScheduleModificationsByDate(DateOnly date)
    {
        var scheduleModifications = clinicDataBase.ScheduleModificationsTable
            .Where(mod => mod.Date == date)
            .AsQueryable();

        return scheduleModifications;
    }

    public IQueryable<ScheduleModification> GetScheduleModificationsForDoctorOnDate(int doctorId, DateOnly date)
    {
        var scheduleModifications = clinicDataBase.ScheduleModificationsTable
            .Where(mod => mod.Date == date && mod.DoctorId == doctorId)
            .AsQueryable();

        return scheduleModifications;
    }

    public async Task UpdateScheduleModification(ScheduleModification modification)
    {
        clinicDataBase.ScheduleModificationsTable.Update(modification);
        await clinicDataBase.SaveChangesAsync();
    }

    public async Task DeleteScheduleModification(int id)
    {
        var modification = await this.GetScheduleModification(id);
        if (modification != null)
        {
            clinicDataBase.ScheduleModificationsTable.Remove(modification);
            await clinicDataBase.SaveChangesAsync();
        }
    }

    // Clinic hours CRUD
    public async Task AddClinicHours(ClinicDayBussinesHours clinicHours)
    {
        await clinicDataBase.ClinicDayBussinesHoursTable.AddAsync(clinicHours);
        await clinicDataBase.SaveChangesAsync();
    }

    public async Task<ClinicDayBussinesHours?> GetClinicHours(int id)
    {
        return await clinicDataBase.ClinicDayBussinesHoursTable.FindAsync(id);
    }

    public IQueryable<ClinicDayBussinesHours> GetClinicHoursList()
    {
        return clinicDataBase.ClinicDayBussinesHoursTable.AsQueryable();
    }

    public async Task<ClinicDayBussinesHours?> GetClinicHoursByDay(DayOfWeek dayOfWeek)
    {
        return await clinicDataBase.ClinicDayBussinesHoursTable
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

        clinicDataBase.ClinicDayBussinesHoursTable.Update(existingClinicHours);
        await clinicDataBase.SaveChangesAsync();
    }

    public async Task DeleteClinicHours(int id)
    {
        var clinicDayBussinesHours = await this.GetClinicHours(id);
        if (clinicDayBussinesHours != null)
        {
            clinicDataBase.ClinicDayBussinesHoursTable.Remove(clinicDayBussinesHours);
            await clinicDataBase.SaveChangesAsync();
        }
    }
}
