namespace ClinicaDental.ApiService.Appointments.Services;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;

using Npgsql.Replication;

public class AppointmentCalendar
{
    private readonly AppointmentRegistry appointmentRegistry;
    private readonly ScheduleRegistry scheduleRegistry;
    private readonly DoctorRegistry doctorRegistry;

    public AppointmentCalendar(AppointmentRegistry appointmentRegistry, ScheduleRegistry scheduleRegistry, DoctorRegistry doctorRegistry)
    {
        this.appointmentRegistry = appointmentRegistry;
        this.scheduleRegistry = scheduleRegistry;
        this.doctorRegistry = doctorRegistry;
    }

    public async Task<IEnumerable<TimeOnly>> GetAvailableTimeSlots(int doctorId, DateOnly date)
    {
        var doctor = await this.doctorRegistry.GetDoctorWithId(doctorId);

        if (doctor is null)
        {
            return Enumerable.Empty<TimeOnly>();
        }

        var dayOfWeek = date.DayOfWeek;
        var doctorSchedule = await this.GetDoctorschedule(doctorId, dayOfWeek);

        if (doctorSchedule is null || doctorSchedule.IsOff)
        {
            return Enumerable.Empty<TimeOnly>();
        }

        var scheduleModifications = await this.scheduleRegistry.GetScheduleModificationsForDoctorOnDate(doctorId, date);

        var existingAppointments = await this.appointmentRegistry.GetAppointmentsListByDate(date);

        var availableSlots = new List<TimeOnly>();
        var currentTime = doctorSchedule.StartTime;

        while (currentTime < doctorSchedule.EndTime)
        {
            var slotIsOccupied = existingAppointments
                .Any(appointment => currentTime.IsBetween(appointment.StartTime, appointment.EndTime()));

            if (slotIsOccupied)
            {
                currentTime = currentTime.AddHours(1);
                continue;
            }

            var slotIsUnavailable = scheduleModifications
                .Any(modification => currentTime.IsBetween(modification.StartTime, modification.EndTime));

            if (slotIsUnavailable)
            {
                currentTime = currentTime.AddHours(1);
                continue;
            }

            availableSlots.Add(currentTime);
            currentTime = currentTime.AddHours(1);
        }

        return availableSlots;
    }

    public async Task<Appointment?> GetAppointmentById(int appointmentId)
    {
        var appointment = await this.appointmentRegistry.GetAppointmentById(appointmentId);

        return appointment;
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsForDoctorInRange(int doctorId, DateOnly dateStart, DateOnly dateEnd)
    {
        var doctorAppointments = await this.appointmentRegistry.GetAppointmentsListByDoctor(doctorId);

        var appointmentsInRange = doctorAppointments
            .Where(appointment => IsDatweBetween(appointment.Date, dateStart, dateEnd));

        return appointmentsInRange;
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsInRange(DateOnly dateStart, DateOnly dateEnd)
    {
        var appointments = await this.appointmentRegistry.GetAppointmentsList();

        var appointmentsInRange = appointments
            .Where(appointment => IsDatweBetween(appointment.Date, dateStart, dateEnd));

        return appointmentsInRange;
    }

    private static bool IsDatweBetween(DateOnly date, DateOnly dateStart, DateOnly dateEnd)
    {
        var isBetween = date >= dateStart && date <= dateEnd;

        return isBetween;
    }

    private async Task<DoctorSchedule?> GetDoctorschedule(int doctorId, DayOfWeek dayOfWeek)
    {
        var doctor = await this.doctorRegistry.GetDoctorWithId(doctorId);

        if (doctor is not null)
        {
            var doctorSchedule = doctor.Schedules
                .Find(schedule => schedule.DayOfWeek == dayOfWeek);

            return doctorSchedule;
        }
        else
        {
            return null;
        }
    }
}
