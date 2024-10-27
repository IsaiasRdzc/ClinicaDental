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
        // TODO: Implement scheduling logic
    }

    public async Task ReScheduleAppointment(int appointmentId, DateOnly date, TimeOnly time, int duration)
    {
        // TODO: Implement rescheduling logic
    }

    public async Task DeleteAppointment(int appointmentId)
    {
        // TODO: Implement rescheduling logic
    }
}
