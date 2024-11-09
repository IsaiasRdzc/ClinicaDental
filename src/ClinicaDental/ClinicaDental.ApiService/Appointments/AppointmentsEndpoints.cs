namespace ClinicaDental.ApiService.Appointments;

using ClinicaDental.ApiService.Appointments.Services;
using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;

using Microsoft.AspNetCore.Mvc;

public static class AppointmentsEndpoints
{
    public static void MapAppointmentsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/appointments");

        group.MapPost(string.Empty, ScheduleAppointment);
        group.MapPost("doctor", SetDoctorSchedule);
        group.MapPost("clinicHours", SetClinicHours);
        group.MapPost("initializeDoctor", CreateDoctorAccount);

        group.MapGet("{id}", GetAppointment);
        group.MapGet(string.Empty, GetAppointmentsInRange);
        group.MapGet("doctor/{doctorId}", GetAppointmentsForDoctorInRange);
        group.MapGet("availableSlots", GetAvailableSlots);
        group.MapGet("clinicHours", GetClinicHours);

        group.MapPut("reschedule/{id}", ReScheduleAppointment);

        group.MapDelete("{id}", DeleteAppointment);
    }

    public static async Task<IResult> CreateDoctorAccount(
        Doctor doctor,
        ClinicAdmin clinicAdmin)
    {
        await clinicAdmin.CreateDoctorAccount(doctor);
        return Results.Ok();
    }

    public static async Task<IResult> GetAvailableSlots(
        int doctorId,
        DateOnly date,
        ClinicAgenda appointmentCalendar)
    {
        var appointments = await appointmentCalendar.GetAvailableTimeSlotsForDoctorInDate(doctorId, date);

        return Results.Ok(appointments);
    }

    public static async Task<IResult> GetAppointmentsInRange(
        DateOnly dateStart,
        DateOnly dateEnd,
        ClinicAgenda appointmentCalendar)
    {
        var appointments = await appointmentCalendar.GetAllAppointmentsInRange(dateStart, dateEnd);

        return Results.Ok(appointments);
    }

    public static async Task<IResult> GetAppointmentsForDoctorInRange(
        int doctorId,
        DateOnly dateTimeStart,
        DateOnly dateTimeEnd,
        ClinicAgenda appointmentCalendar)
    {
        var appointments = await appointmentCalendar.GetAppointmentsForDoctorInRange(doctorId, dateTimeStart, dateTimeEnd);

        return Results.Ok(appointments);
    }

    public static async Task<IResult> GetAppointment(
        int folio,
        ClinicAgenda appointmentCalendar)
    {
        var appointment = await appointmentCalendar.GetAppointmentByFolio(folio);

        return Results.Ok(appointment);
    }
    //esquema y 
    public static async Task<IResult> ScheduleAppointment(
        [FromBody]
        Appointment appointment,
        [FromServices]
        ClinicReceptionist scheduler)
    {
        await scheduler.ScheduleAppointment(appointment);

        return Results.Ok();
    }

    public static async Task<IResult> ReScheduleAppointment(
        //from body objeto
        int appointmentId,
        DateOnly date,
        TimeOnly time,
        int duration,
        ClinicReceptionist scheduler)
    {
        await scheduler.ReScheduleAppointment(appointmentId, date, time, duration);

        return Results.Ok();
    }

    public static async Task<IResult> DeleteAppointment(
        int appointmentId,
        ClinicReceptionist scheduler)
    {
        await scheduler.CancelAppointment(appointmentId);

        return Results.Ok();
    }

    public static async Task<IResult> SetClinicHours(
        ClinicDayBussinesHours clinicHours,
        ClinicAdmin clinicAdmin)
    {
        await clinicAdmin.SetClinicBussinesHours(clinicHours);
        return Results.Ok();
    }

    public static async Task<IResult> GetClinicHours(
        ClinicAdmin clinicAdmin)
    {
        var clinicHours = await clinicAdmin.GetClinicBussinesHours();
        return Results.Ok(clinicHours);
    }

    public static async Task<IResult> SetDoctorSchedule(
        DoctorDaySchedule doctorSchedule,
        ClinicAdmin clinicAdmin)
    {
        await clinicAdmin.SetDoctorDaySchedule(doctorSchedule);
        return Results.Ok();
    }
}
