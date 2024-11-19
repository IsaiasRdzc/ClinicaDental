namespace ClinicaDental.ApiService.Appointments.Services;

using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;

public class ClinicAgenda
{
    private readonly AppointmentRegistry appointmentRegistry;
    private readonly ScheduleRegistry scheduleRegistry;
    private readonly DoctorRegistry doctorRegistry;

    public ClinicAgenda(AppointmentRegistry appointmentRegistry, ScheduleRegistry scheduleRegistry, DoctorRegistry doctorRegistry)
    {
        this.appointmentRegistry = appointmentRegistry;
        this.scheduleRegistry = scheduleRegistry;
        this.doctorRegistry = doctorRegistry;
    }

    public async Task<bool> IsAppointmentSlotAvailable(Appointment appointment)
    {
        var availableSlots = await this.GetAvailableTimeSlotsForDoctorInDate(appointment.DoctorId, appointment.Date);
        var requestedStartTime = appointment.StartTime;

        if (!availableSlots.Contains(requestedStartTime))
        {
            return false;
        }

        // Check if the next hours are available based on the appointment's duration
        TimeOnly endTime = requestedStartTime.AddHours(appointment.DurationInHours);
        for (TimeOnly currentTime = requestedStartTime; currentTime < endTime; currentTime = currentTime.AddHours(1))
        {
            if (!availableSlots.Contains(currentTime))
            {
                return false;
            }
        }

        return true;
    }

    public async Task<List<TimeOnly>> GetAvailableTimeSlotsForDoctorInDate(int doctorId, DateOnly date)
    {
        var doctor = await this.doctorRegistry.GetDoctorWithId(doctorId);

        if (doctor is null)
        {
            return new List<TimeOnly>();
        }

        var dayOfWeek = date.DayOfWeek;
        var doctorSchedule = await this.GetDoctorschedule(doctorId, dayOfWeek);

        if (doctorSchedule is null || doctorSchedule.IsOff)
        {
            return new List<TimeOnly>();
        }

        var scheduleModifications = await this.scheduleRegistry.GetScheduleModificationsForDoctorOnDate(doctorId, date);

        var existingAppointments = await this.appointmentRegistry.GetAppointmentsListByDate(date);

        var availableSlots = new List<TimeOnly>();
        var currentTime = doctorSchedule.StartTime;

        while (currentTime < doctorSchedule.EndTime)
        {
            var slotIsOccupied = existingAppointments
                .Exists(appointment => currentTime.IsBetween(appointment.StartTime, appointment.EndTime));

            if (slotIsOccupied)
            {
                currentTime = currentTime.AddHours(1);
                continue;
            }

            var slotIsUnavailable = scheduleModifications
                .Exists(modification => currentTime.IsBetween(modification.StartTime, modification.EndTime));

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

    public async Task<Appointment?> GetAppointmentByFolio(int folio)
    {
        var appointment = await this.appointmentRegistry.GetAppointmentByFolio(folio);

        return appointment;
    }

    public async Task<List<Appointment>> GetAppointmentsForDoctorInDate(int doctorId, DateOnly date)
    {
        var doctorAppointments = await this.appointmentRegistry.GetAppointmentsListByDoctor(doctorId);

        var appointmentsInDate = doctorAppointments
            .Where(appointment => appointment.Date == date).ToList();

        return appointmentsInDate;
    }

    public async Task<List<Appointment>> GetAppointmentsForDoctorInRange(int doctorId, DateOnly dateStart, DateOnly dateEnd)
    {
        var doctorAppointments = await this.appointmentRegistry.GetAppointmentsListByDoctor(doctorId);

        var appointmentsInRange = doctorAppointments
            .Where(appointment => IsDateBetween(appointment.Date, dateStart, dateEnd)).ToList();

        return appointmentsInRange;
    }

    public async Task<List<Appointment>> GetAllAppointmentsInDate(DateOnly date)
    {
        var appointments = await this.appointmentRegistry.GetAppointmentsList();

        var appointmentsInDate = appointments
            .Where(appointment => appointment.Date == date).ToList();

        return appointmentsInDate;
    }

    public async Task<List<Appointment>> GetAllAppointmentsInRange(DateOnly dateStart, DateOnly dateEnd)
    {
        var appointments = await this.appointmentRegistry.GetAppointmentsList();

        var appointmentsInRange = appointments
            .Where(appointment => IsDateBetween(appointment.Date, dateStart, dateEnd)).ToList();

        return appointmentsInRange;
    }

    // Private functions
    private static bool IsDateBetween(DateOnly date, DateOnly dateStart, DateOnly dateEnd)
    {
        var isBetween = date >= dateStart && date <= dateEnd;

        return isBetween;
    }

    private async Task<DoctorDaySchedule?> GetDoctorschedule(int doctorId, DayOfWeek dayOfWeek)
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
