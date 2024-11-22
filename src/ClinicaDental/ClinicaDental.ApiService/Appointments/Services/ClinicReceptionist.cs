namespace ClinicaDental.ApiService.Appointments.Services;

using ClinicaDental.ApiService.DataBase.Models.Appointments;
using ClinicaDental.ApiService.DataBase.Registries.Appointments;
using ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;

public class ClinicReceptionist
{
    private readonly AppointmentsRegistry appointmentRegistry;
    private readonly ClinicAgenda clinicAgenda;
    private readonly PatientsRegistry patientsRegistry;

    public ClinicReceptionist(AppointmentsRegistry appointmentRegistry, PatientsRegistry patientsRegistry, ClinicAgenda clinicAgenda)
    {
        this.appointmentRegistry = appointmentRegistry;
        this.clinicAgenda = clinicAgenda;
        this.patientsRegistry = patientsRegistry;
    }

    public async Task<int> ScheduleAppointment(Appointment appointment)
    {
        var appointmentSlotIsAvailable = await this.clinicAgenda.IsAppointmentSlotAvailable(appointment);

        if (!this.IsPatientIdValid(appointment.PatientId))
        {
            throw new InvalidOperationException("Patient does not exists");
        }
        else if (!appointmentSlotIsAvailable)
        {
            throw new InvalidOperationException("The requested appointment slot is not available.");
        }

        await this.appointmentRegistry.CreateAppointment(appointment);
        return appointment.Folio;
    }

    public async Task ReScheduleAppointment(int folio, DateOnly date, TimeOnly time, int duration)
    {
        var existingAppointment = await this.appointmentRegistry.GetAppointmentByFolio(folio);
        if (existingAppointment is null)
        {
            throw new KeyNotFoundException("Appointment not found.");
        }

        existingAppointment.Date = date;
        existingAppointment.StartTime = time;
        existingAppointment.DurationInHours = duration;

        var appointmentSlotIsAvailable = await this.clinicAgenda.IsAppointmentSlotAvailable(existingAppointment);
        if (!appointmentSlotIsAvailable)
        {
            throw new InvalidOperationException("The requested appointment slot is not available.");
        }

        await this.appointmentRegistry.UpdateAppointment(folio, date, time, duration);
    }

    public async Task UpdateAppointmentPatientId(int folio, int newPatientId)
    {
        var existingAppointment = await this.appointmentRegistry.GetAppointmentByFolio(folio);
        if (existingAppointment is null)
        {
            throw new KeyNotFoundException("Appointment not found.");
        }

        await this.appointmentRegistry.UpdateAppointmentPatientId(folio, newPatientId);
    }

    public async Task CancelAppointment(int appointmentId)
    {
        var existingAppointment = await this.appointmentRegistry.GetAppointmentByFolio(appointmentId);
        if (existingAppointment is null)
        {
            throw new KeyNotFoundException("Appointment not found.");
        }

        await this.appointmentRegistry.DeleteAppointment(appointmentId);
    }

    private bool IsPatientIdValid(int patientId)
    {
        if (patientId == 0)
        {
            return true;
        }
        else
        {
            return this.patientsRegistry.GetPatientByPatientId(patientId) is not null;
        }
    }
}
