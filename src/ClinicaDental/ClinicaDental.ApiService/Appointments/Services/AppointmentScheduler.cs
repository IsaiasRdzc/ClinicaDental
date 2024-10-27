namespace ClinicaDental.ApiService.Appointments.Services;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;

public class AppointmentScheduler
{
    private readonly AppointmentRegistry appointmentRegistry;
    private readonly AppointmentCalendar appointmentCalendar;

    public AppointmentScheduler(
        AppointmentRegistry appointmentRegistry,
        AppointmentCalendar appointmentCalendar)
    {
        this.appointmentRegistry = appointmentRegistry;
        this.appointmentCalendar = appointmentCalendar;
    }

    public async Task ScheduleAppointment(Appointment appointment)
    {
        var appointmentCanBeScheduled = await this.appointmentCalendar.CheckAppointmentValidity(appointment);

        if (!appointmentCanBeScheduled)
        {
            throw new InvalidOperationException("The requested appointment slot is not available.");
        }

        await this.appointmentRegistry.CreateAppointment(appointment);
    }

    public async Task ReScheduleAppointment(int appointmentId, DateOnly date, TimeOnly time, int duration)
    {
        var existingAppointment = await this.appointmentRegistry.GetAppointmentById(appointmentId);
        if (existingAppointment is null)
        {
            throw new KeyNotFoundException("Appointment not found.");
        }

        await this.CancelAppointment(appointmentId);

        var newAppointment = existingAppointment;
        newAppointment.Date = date;
        newAppointment.StartTime = time;
        newAppointment.Duration = duration;

        var appointmentCanBeScheduled = await this.appointmentCalendar.CheckAppointmentValidity(newAppointment);
        if (!appointmentCanBeScheduled)
        {
            await this.ScheduleAppointment(existingAppointment);
            throw new InvalidOperationException("The requested appointment slot is not available.");
        }

        await this.appointmentRegistry.UpdateAppointment(appointmentId, date, time, duration);
    }

    public async Task CancelAppointment(int appointmentId)
    {
        var existingAppointment = await this.appointmentRegistry.GetAppointmentById(appointmentId);
        if (existingAppointment is null)
        {
            throw new KeyNotFoundException("Appointment not found.");
        }

        await this.appointmentRegistry.DeleteAppointment(appointmentId);
    }
}
