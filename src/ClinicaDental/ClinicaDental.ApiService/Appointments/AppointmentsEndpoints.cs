namespace ClinicaDental.ApiService.Appointments;

using ClinicaDental.ApiService.Appointments.Services;
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
        return await ErrorOrResultHandler.HandleResult(async () => await clinicAdmin.CreateDoctorAccount(doctor));
    }

    public static async Task<IResult> GetAvailableSlots(
        int doctorId,
        DateOnly date,
        ClinicAgenda appointmentCalendar)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await appointmentCalendar.GetAvailableTimeSlotsForDoctorInDate(doctorId, date));
    }

    public static async Task<IResult> GetAppointmentsInRange(
        DateOnly dateStart,
        DateOnly dateEnd,
        ClinicAgenda appointmentCalendar)
    {
        return await ErrorOrResultHandler.HandleResult(
            async () => await appointmentCalendar.GetAllAppointmentsInRange(dateStart, dateEnd));
    }

    public static async Task<IResult> GetAppointmentsForDoctorInRange(
        int doctorId,
        DateOnly dateTimeStart,
        DateOnly dateTimeEnd,
        ClinicAgenda appointmentCalendar)
    {
        return await ErrorOrResultHandler.HandleResult(
            async () => await appointmentCalendar.GetAppointmentsForDoctorInRange(doctorId, dateTimeStart, dateTimeEnd));
    }

    public static async Task<IResult> GetAppointment(
        int folio,
        ClinicAgenda appointmentCalendar)
    {
        return await ErrorOrResultHandler.HandleResult(
            async () => await appointmentCalendar.GetAppointmentByFolio(folio));
    }

    public static async Task<IResult> ScheduleAppointment(
        Appointment appointment,
        ClinicReceptionist scheduler)
    {
        return await ErrorOrResultHandler.HandleResult(
            async () => await scheduler.ScheduleAppointment(appointment));
    }

    public static async Task<IResult> ReScheduleAppointment(
        int appointmentId,
        DateOnly date,
        TimeOnly time,
        int duration,
        ClinicReceptionist scheduler)
    {
        return await ErrorOrResultHandler.HandleResult(
            async () => await scheduler.ReScheduleAppointment(appointmentId, date, time, duration));
    }

    public static async Task<IResult> DeleteAppointment(
        int appointmentId,
        ClinicReceptionist scheduler)
    {
        return await ErrorOrResultHandler.HandleResult(
            async () => await scheduler.CancelAppointment(appointmentId));
    }

    public static async Task<IResult> SetClinicHours(
        ClinicDayBussinesHours clinicHours,
        ClinicAdmin clinicAdmin)
    {
        return await ErrorOrResultHandler.HandleResult(
            async () => await clinicAdmin.SetClinicBussinesHours(clinicHours));
    }

    public static async Task<IResult> GetClinicHours(
        ClinicAdmin clinicAdmin)
    {
        return await ErrorOrResultHandler.HandleResult(
            async () => await clinicAdmin.GetClinicBussinesHours());
    }

    public static async Task<IResult> SetDoctorSchedule(
        DoctorDaySchedule doctorSchedule,
        ClinicAdmin clinicAdmin)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await clinicAdmin.SetDoctorDaySchedule(doctorSchedule));
    }

    public static async Task<IResult> GetDoctorsList(
        ClinicAdmin clinicAdmin)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await clinicAdmin.GetDoctorsList());
    }
}
