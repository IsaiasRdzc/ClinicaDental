namespace ClinicaDental.ApiService.Appointments;

using ClinicaDental.ApiService.DataBase.Models;

public class AppointmentAlertSystem
{
    public void SendAppointmentReminder(Appointment appointment)
    {
        // TODO remind patient of appointment
    }

    public void ConfirmAppointmentScheduling(Appointment appointment)
    {
        // TODO confirm the appointment was scheduled correctly
    }

    public void ConfirmAppointmentReScheduling(Appointment appointment)
    {
        // TODO confirm the appointment was rescheduled correctly
    }

    public void ConfirmAppointmentCancelation(Appointment appointment)
    {
        // TODO confirm the appointment was canceled correctly
    }
}
