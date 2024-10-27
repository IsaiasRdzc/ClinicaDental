namespace ClinicaDental.ApiService.Appointments;

using ClinicaDental.ApiService.Appointments.Services;
using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;

public static class AppointmentsEndpoints
{
    //tiene nombre de implementación
    public static void MapAppointmentsEndpoints(this IEndpointRouteBuilder app)
    {
        //grupo de que
        var group = app.MapGroup("api/appointments");

        group.MapPost(string.Empty, ScheduleAppointment);
        group.MapPost("doctor", SetDoctorSchedule);
        group.MapPost("clinicHours", SetClinicHours);

        group.MapGet("{id}", GetAppointment);
        group.MapGet(string.Empty, GetAppointmentsInRange);
        group.MapGet("doctor/{doctorId}", GetAppointmentsForDoctorInRange);
        group.MapGet("availableSlots", GetAvailableSlots);
        group.MapGet("clinicHours", GetClinicHours);

        group.MapPut("reschedule/{id}", ReScheduleAppointment);

        group.MapDelete("{id}", DeleteAppointment);
        group.MapPost("initializeDoctor", TEMP_InitializeDoctor);
    }

    //TEMP es una abreviatura
    public static async Task<IResult> TEMP_InitializeDoctor(
        Doctor doctor,
        WorkScheduleAdmin workScheduleAdmin)
    {
        await workScheduleAdmin.TEMP_InitializeDoctorAccount(doctor);
        return Results.Ok();
    }

    public static async Task<IResult> GetAvailableSlots(
        int doctorId,
        DateOnly date,
        AppointmentCalendar appointmentCalendar)
    {
        var appointments = await appointmentCalendar.GetAvailableTimeSlots(doctorId, date);

    //si el método se llama GetAvailableSlots signifca que devuelve slots no appointments
        return Results.Ok(appointments);
    }

    public static async Task<IResult> GetAppointmentsInRange(
        DateOnly dateStart,
        DateOnly dateEnd,
        AppointmentCalendar appointmentCalendar)
    {
        var appointments = await appointmentCalendar.GetAppointmentsInRange(dateStart, dateEnd);

        //aqui si
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
        await scheduler.CancelAppointment(appointmentId);

        return Results.Ok();
    }

    public static async Task<IResult> SetClinicHours(
        ClinicHours clinicHours,
        WorkScheduleAdmin workScheduleAdmin)
    {
        await workScheduleAdmin.SetClinicHours(clinicHours);
        return Results.Ok();
    }

    public static async Task<IResult> GetClinicHours(
        WorkScheduleAdmin workScheduleAdmin)
    {
        var clinicHours = await workScheduleAdmin.GetClinicHours();
        return Results.Ok(clinicHours);
    }

    public static async Task<IResult> SetDoctorSchedule(
        DoctorDaySchedule doctorSchedule,
        WorkScheduleAdmin workScheduleAdmin)
    {
        await workScheduleAdmin.SetDoctorSchedule(doctorSchedule);
        return Results.Ok();
    }
}
