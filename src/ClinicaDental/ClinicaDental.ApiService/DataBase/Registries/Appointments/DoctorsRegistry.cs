namespace ClinicaDental.ApiService.DataBase.Registries;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models.Appointments;

using Microsoft.EntityFrameworkCore;

public class DoctorsRegistry(AppDbContext clinicDataBase)
{
    public IQueryable<Doctor> GetDoctorsList()
    {
        return clinicDataBase.DoctorsTable.AsQueryable();
    }

    public async Task<Doctor?> GetDoctorWithId(int id)
    {
        return await clinicDataBase.DoctorsTable
        .Include(doctor => doctor.Schedules)
        .FirstOrDefaultAsync(doctor => doctor.Id == id);
    }

    public async Task AddDoctor(Doctor doctor)
    {
        clinicDataBase.DoctorsTable.Add(doctor);
        await clinicDataBase.SaveChangesAsync();
    }

    public async Task UpdateDoctor(int id, Doctor doctor)
    {
        if (!this.DoctorExists(id))
        {
            throw new KeyNotFoundException($"Doctor with ID {id} not found.");
        }

        clinicDataBase.Entry(doctor).State = EntityState.Modified;
        await clinicDataBase.SaveChangesAsync();
    }

    public async Task RemoveDoctor(int id)
    {
        var doctor = await this.GetDoctorWithId(id);

        if (doctor is null)
        {
            throw new KeyNotFoundException($"Doctor with ID {id} not found.");
        }

        clinicDataBase.DoctorsTable.Remove(doctor);
        await clinicDataBase.SaveChangesAsync();
    }

    private bool DoctorExists(int id)
    {
        return clinicDataBase.DoctorsTable.Any(e => e.Id == id);
    }
}
