namespace ClinicaDental.ApiService.Appointments;

using ClinicaDental.ApiService.Appointments.Services;
using ClinicaDental.ApiService.DataBase.Models.Appointments;
using ClinicaDental.ApiService.DataBase.Models.HumanResources;

public static class AppointmentsEndpoints
{
    public static void MapAppointmentsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/appointments");

        group.MapPost(string.Empty, ScheduleAppointment);
        group.MapPost("clinicHours", SetClinicHours);

        group.MapGet("{id}", GetAppointment);
        group.MapGet(string.Empty, GetAppointmentsInRange);
        group.MapGet("doctor/{doctorId}", GetAppointmentsForDoctorInRange);
        group.MapGet("availableSlots", GetAvailableSlots);
        group.MapGet("clinicHours", GetClinicHours);

        group.MapPut("reschedule/{id}", ReScheduleAppointment);
        group.MapPut("appointment/{appointmentId,newPatientId}", UpdateAppointmentPatientId);

        group.MapDelete("{id}", DeleteAppointment);
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
        ClinicReceptionist receptionist)
    {
        return await ErrorOrResultHandler.HandleResult(
            async () => await receptionist.ScheduleAppointment(appointment));
    }

    public static async Task<IResult> ReScheduleAppointment(
        int appointmentId,
        DateOnly date,
        TimeOnly time,
        int duration,
        ClinicReceptionist receptionist)
    {
        return await ErrorOrResultHandler.HandleResult(
            async () => await receptionist.ReScheduleAppointment(appointmentId, date, time, duration));
    }

    public static async Task<IResult> UpdateAppointmentPatientId(
        int folio,
        int newPatientId,
        ClinicReceptionist receptionist)
    {
        return await ErrorOrResultHandler.HandleResult(
            async () => await receptionist.UpdateAppointmentPatientId(folio, newPatientId));
    }

    public static async Task<IResult> DeleteAppointment(
        int appointmentId,
        ClinicReceptionist receptionist)
    {
        return await ErrorOrResultHandler.HandleResult(
            async () => await receptionist.CancelAppointment(appointmentId));
    }

    public static async Task<IResult> SetClinicHours(
        ClinicDayBussinesHours clinicHours,
        ClinicSchedulingAdmin schedulingAdmin)
    {
        return await ErrorOrResultHandler.HandleResult(
            async () => await schedulingAdmin.SetClinicBussinesHours(clinicHours));
    }

    public static async Task<IResult> GetClinicHours(
        ClinicSchedulingAdmin schedulingAdmin)
    {
        return await ErrorOrResultHandler.HandleResult(
            async () => await schedulingAdmin.GetClinicBussinesHours());
    }
}
