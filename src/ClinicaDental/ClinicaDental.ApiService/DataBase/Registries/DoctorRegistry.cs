namespace ClinicaDental.ApiService.DataBase.Registries;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;
using Microsoft.EntityFrameworkCore;

public class DoctorRegistry(AppDbContext context)
{
    public IQueryable<Doctor> GetDoctorsList()
    {
        return context.Doctors.AsQueryable();
    }

    public async Task<Doctor?> GetDoctorWithId(int id)
    {
        return await context.Doctors
        .Include(doctor => doctor.Schedules)
        .FirstOrDefaultAsync(doctor => doctor.Id == id);
    }

    public async Task AddDoctor(Doctor doctor)
    {
        context.Doctors.Add(doctor);
        await context.SaveChangesAsync();
    }

    public async Task UpdateDoctor(int id, Doctor doctor)
    {
        if (!this.DoctorExists(id))
        {
            throw new KeyNotFoundException($"Doctor with ID {id} not found.");
        }

        context.Entry(doctor).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task RemoveDoctor(int id)
    {
        var doctor = await this.GetDoctorWithId(id);

        if (doctor is null)
        {
            throw new KeyNotFoundException($"Doctor with ID {id} not found.");
        }

        context.Doctors.Remove(doctor);
        await context.SaveChangesAsync();
    }

    private bool DoctorExists(int id)
    {
        return context.Doctors.Any(e => e.Id == id);
    }
}
