namespace ClinicaDental.ApiService.Appointments;

using ClinicaDental.ApiService.Appointments.Services;
using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;

public static class AppointmentsEndpoints
{
    public static void MapAppointmentsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/appointments");

        group.MapPost(string.Empty, ScheduleAppointment);

        group.MapGet("{id}", GetAppointment);
        group.MapGet(string.Empty, GetAppointmentsInRange);
        group.MapGet("doctor/{doctorId}", GetAppointmentsForDoctorInRange);

        group.MapPut("reschedule/{id}", ReScheduleAppointment);

        group.MapDelete("{id}", DeleteAppointment);
    }

    public static async Task<IResult> GetAvailableSlots(
        int doctorId,
        DateOnly date,
        AppointmentCalendar appointmentCalendar)
    {
        var appointments = await appointmentCalendar.GetAvailableTimeSlots(doctorId, date);

        return Results.Ok(appointments);
    }

    public static async Task<IResult> GetAppointmentsInRange(
        DateOnly dateStart,
        DateOnly dateEnd,
        AppointmentCalendar appointmentCalendar)
    {
        var appointments = await appointmentCalendar.GetAppointmentsInRange(dateStart, dateEnd);

        return Results.Ok(appointments);
    }

    public static async Task<IResult> GetAppointmentsForDoctorInRange(
        int doctorId,
        DateOnly dateTimeStart,
        DateOnly dateTimeEnd,
        AppointmentCalendar appointmentCalendar)
    {
        var appointments = await appointmentCalendar.GetAppointmentsForDoctorInRange(doctorId, dateTimeStart, dateTimeEnd);

        return Results.Ok(appointments);
    }

    public static async Task<IResult> GetAppointment(
        int appointmentId,
        AppointmentCalendar appointmentCalendar)
    {
        var appointment = await appointmentCalendar.GetAppointmentById(appointmentId);

        return Results.Ok(appointment);
    }

    public static async Task<IResult> ScheduleAppointment(
        Appointment appointment,
        AppointmentScheduler scheduler)
    {
        await scheduler.ScheduleAppointment(appointment);

        return Results.Ok();
    }

    public static async Task<IResult> ReScheduleAppointment(
        int appointmentId,
        DateOnly date,
        TimeOnly time,
        int duration,
        AppointmentScheduler scheduler)
    {
        await scheduler.ReScheduleAppointment(appointmentId, date, time, duration);

        return Results.Ok();
    }

    public static async Task<IResult> DeleteAppointment(
        int appointmentId,
        AppointmentScheduler scheduler)
    {
        await scheduler.DeleteAppointment(appointmentId);

        return Results.Ok();
    }
}
