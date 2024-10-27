namespace ClinicaDental.ApiService.DataBase.Registries;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;
using Microsoft.EntityFrameworkCore;

public class DoctorRegistry
{
    private readonly AppDbContext context;

    public DoctorRegistry(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Doctor>> GetDoctorsList()
    {
        return await this.context.Doctors.ToListAsync();
    }

    public async Task<Doctor?> GetDoctorWithId(int id)
    {
        return await this.context.Doctors
        .Include(doctor => doctor.Schedules)
        .FirstOrDefaultAsync(doctor => doctor.Id == id);
    }

    public async Task AddDoctor(Doctor doctor)
    {
        this.context.Doctors.Add(doctor);
        await this.context.SaveChangesAsync();
    }

    public async Task UpdateDoctor(int id, Doctor doctor)
    {
        if (!this.DoctorExists(id))
        {
            throw new KeyNotFoundException($"Doctor with ID {id} not found.");
        }

        this.context.Entry(doctor).State = EntityState.Modified;
        await this.context.SaveChangesAsync();
    }

    public async Task RemoveDoctor(int id)
    {
        var doctor = await this.GetDoctorWithId(id);

        if (doctor is null)
        {
            throw new KeyNotFoundException($"Doctor with ID {id} not found.");
        }

        this.context.Doctors.Remove(doctor);
        await this.context.SaveChangesAsync();
    }

    private bool DoctorExists(int id)
    {
        return this.context.Doctors.Any(e => e.Id == id);
    }
}
