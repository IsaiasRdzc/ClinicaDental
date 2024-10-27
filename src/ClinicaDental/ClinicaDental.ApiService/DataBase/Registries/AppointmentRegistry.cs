namespace ClinicaDental.ApiService.DataBase.Registries;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;
using Microsoft.EntityFrameworkCore;

public class AppointmentRegistry
{
    private readonly AppDbContext context;

    public AppointmentRegistry(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsList()
    {
        return await this.context.Appointments.ToListAsync();
    }

    public async Task<Appointment?> GetAppointmentById(int id)
    {
        var appointment = await this.context.Appointments.FindAsync(id);
        return appointment;
    }

    public async Task CreateAppointment(Appointment appointment)
    {
        this.context.Appointments.Add(appointment);
        await this.context.SaveChangesAsync();
    }

    public async Task UpdateAppointment(int id, DateOnly date, TimeSlot time)
    {
        var appointment = await this.GetAppointmentById(id);

        if (appointment is null)
        {
            throw new KeyNotFoundException($"Appointment with ID {id} not found.");
        }

        appointment.Date = date;
        appointment.Time = time;

        this.context.Appointments.Update(appointment);
        await this.context.SaveChangesAsync();
    }

    public async Task DeleteAppointment(int id)
    {
        var appointment = await this.GetAppointmentById(id);

        if (appointment is null)
        {
            throw new KeyNotFoundException($"Appointment with ID {id} not found.");
        }

        this.context.Appointments.Remove(appointment);
        await this.context.SaveChangesAsync();
    }
}
