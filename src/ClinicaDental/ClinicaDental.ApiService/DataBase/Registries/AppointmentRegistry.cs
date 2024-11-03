namespace ClinicaDental.ApiService.DataBase.Registries;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;

public class AppointmentRegistry
{
    private readonly AppDbContext context;

    public AppointmentRegistry(AppDbContext context)
    {
        this.context = context;
    }

    public IQueryable GetAppointmentsList()
    {
        var appointmentsList = this.context.Appointments.AsQueryable();
        return appointmentsList;
    }

    public IQueryable GetAppointmentsListByDate(DateOnly date)
    {
        var appointments = this.context.Appointments
        .Where(appointment => appointment.Date == date)
        .AsQueryable();

        return appointments;
    }

    public IQueryable GetAppointmentsListByDoctor(int doctorId)
    {
        var appointments = this.context.Appointments
        .Where(appointment => appointment.DoctorId == doctorId)
        .AsQueryable();

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
