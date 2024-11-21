namespace ClinicaDental.ApiService.Appointments.Services;

using ClinicaDental.ApiService.DataBase.Models.Appointments;

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
        group.MapGet("doctor", GetDoctorsList);
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
        try
        {
            await clinicAdmin.CreateDoctorAccount(doctor);
            return Results.Ok();
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
        catch (InvalidOperationException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> GetAvailableSlots(
        int doctorId,
        DateOnly date,
        ClinicAgenda appointmentCalendar)
    {
        try
        {
            var appointments = await appointmentCalendar.GetAvailableTimeSlotsForDoctorInDate(doctorId, date);
            return Results.Ok(appointments);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
        catch (InvalidOperationException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> GetAppointmentsInRange(
        DateOnly dateStart,
        DateOnly dateEnd,
        ClinicAgenda appointmentCalendar)
    {
        try
        {
            var appointments = await appointmentCalendar.GetAllAppointmentsInRange(dateStart, dateEnd);
            return Results.Ok(appointments);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
        catch (InvalidOperationException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> GetAppointmentsForDoctorInRange(
        int doctorId,
        DateOnly dateTimeStart,
        DateOnly dateTimeEnd,
        ClinicAgenda appointmentCalendar)
    {
        try
        {
            var appointments = await appointmentCalendar.GetAppointmentsForDoctorInRange(doctorId, dateTimeStart, dateTimeEnd);
            return Results.Ok(appointments);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
        catch (InvalidOperationException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> GetAppointment(
        int folio,
        ClinicAgenda appointmentCalendar)
    {
        try
        {
            var appointment = await appointmentCalendar.GetAppointmentByFolio(folio);
            return Results.Ok(appointment);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
        catch (InvalidOperationException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> ScheduleAppointment(
        Appointment appointment,
        ClinicReceptionist scheduler)
    {
        try
        {
            await scheduler.ScheduleAppointment(appointment);
            return Results.Ok();
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
        catch (InvalidOperationException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> ReScheduleAppointment(
        int appointmentId,
        DateOnly date,
        TimeOnly time,
        int duration,
        ClinicReceptionist scheduler)
    {
        try
        {
            await scheduler.ReScheduleAppointment(appointmentId, date, time, duration);
            return Results.Ok();
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
        catch (InvalidOperationException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> DeleteAppointment(
        int appointmentId,
        ClinicReceptionist scheduler)
    {
        try
        {
            await scheduler.CancelAppointment(appointmentId);
            return Results.Ok();
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
        catch (InvalidOperationException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> SetClinicHours(
        ClinicDayBussinesHours clinicHours,
        ClinicAdmin clinicAdmin)
    {
        try
        {
            await clinicAdmin.SetClinicBussinesHours(clinicHours);
            return Results.Ok();
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
        catch (InvalidOperationException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> GetClinicHours(
        ClinicAdmin clinicAdmin)
    {
        try
        {
            var clinicHours = await clinicAdmin.GetClinicBussinesHours();
            return Results.Ok(clinicHours);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
        catch (InvalidOperationException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> SetDoctorSchedule(
        DoctorDaySchedule doctorSchedule,
        ClinicAdmin clinicAdmin)
    {
        try
        {
            await clinicAdmin.SetDoctorDaySchedule(doctorSchedule);
            return Results.Ok();
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
        catch (InvalidOperationException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> GetDoctorsList(
        ClinicAdmin clinicAdmin)
    {
        try
        {
            var doctors = await clinicAdmin.GetDoctorsList();
            return Results.Ok(doctors);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
        catch (InvalidOperationException error)
        {
            return Results.BadRequest(error.Message);
        }
    }
}
