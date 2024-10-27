namespace ClinicaDental.ApiService.Appointments;

using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;

public static class AppointmentsServiceEndpoints
{
    public static void MapAppointmentsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/appointments");

        group.MapPost(string.Empty, ScheduleAppointment);

        group.MapGet(string.Empty, GetAppointments);
        group.MapGet("{id}", GetAppointment);

        group.MapPut("reschedule/{id}", ReScheduleAppointment);

        group.MapDelete("{id}", DeleteAppointment);
    }

    public static async Task<IResult> GetAppointments(
        AppointmentRegistry appointmentRegistry)
    {
        var appointments = await appointmentRegistry.GetAppointmentsList();

        return Results.Ok(appointments);
    }

    public static async Task<IResult> GetAppointment(
        int id,
        AppointmentRegistry appointmentRegistry)
    {
        var appointment = await appointmentRegistry.GetAppointmentById(id);

        return Results.Ok(appointment);
    }

    public static async Task<IResult> ScheduleAppointment(
        Appointment appointment,
        AppointmentRegistry appointmentRegistry)
    {
        // TODO: Delegate job to appointment sceduler
        var scheduler = new AppointmentScheduler();
        await scheduler.ScheduleAppointment(appointment);

        return Results.Ok();
    }

    public static async Task<IResult> ReScheduleAppointment(
        int id,
        DateOnly date,
        TimeSlot time,
        AppointmentRegistry appointmentRegistry)
    {
        // TODO: Delegate job to appointment sceduler
        await appointmentRegistry.UpdateAppointment(id, date, time);

        return Results.Ok();
    }

    public static async Task<IResult> DeleteAppointment(
        int id,
        AppointmentRegistry appointmentRegistry)
    {
        // TODO: Delegate job to appointment sceduler
        await appointmentRegistry.DeleteAppointment(id);

        return Results.Ok();
    }
}
