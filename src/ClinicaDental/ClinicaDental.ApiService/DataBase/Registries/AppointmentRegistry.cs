namespace ClinicaDental.ApiService.DataBase.Registries;

using System.Diagnostics;

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

    public async Task<List<Appointment>> GetAppointmentsList()
    {
        var appointmentsList = await this.context.Appointments.ToListAsync();
        return appointmentsList;
    }

    public async Task<List<Appointment>> GetAppointmentsListByDate(DateOnly date)
    {
        var appointments = await this.context.Appointments
        .Where(appointment => appointment.Date == date)
        .ToListAsync();

        return appointments;
    }

    public async Task<List<Appointment>> GetAppointmentsListByDoctor(int doctorId)
    {
        var appointments = await this.context.Appointments
        .Where(appointment => appointment.DoctorId == doctorId)
        .ToListAsync();

        return appointments;
    }

    public async Task<Appointment?> GetAppointmentByFolio(int folio)
    {
        var appointment = await this.context.Appointments.FindAsync(folio);
        return appointment;
    }

    public async Task CreateAppointment(Appointment appointment)
    {
        this.context.Appointments.Add(appointment);
        await this.context.SaveChangesAsync();
    }

    public async Task UpdateAppointment(int folio, DateOnly date, TimeOnly time, int duration)
    {
        var appointment = await this.GetAppointmentByFolio(folio);

        if (appointment is null)
        {
            throw new KeyNotFoundException($"Appointment with ID {folio} not found.");
        }

        appointment.Date = date;
        appointment.StartTime = time;
        appointment.DurationInHours = duration;

        this.context.Appointments.Update(appointment);
        await this.context.SaveChangesAsync();
    }

    public async Task DeleteAppointment(int folio)
    {
        var appointment = await this.GetAppointmentByFolio(folio);

        if (appointment is null)
        {
            throw new KeyNotFoundException($"Appointment with folio {folio} not found.");
        }

        this.context.Appointments.Remove(appointment);
        await this.context.SaveChangesAsync();
    }
}
