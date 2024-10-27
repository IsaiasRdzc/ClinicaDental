/*namespace ClinicaDental.ApiService.Appointments;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;

using Microsoft.EntityFrameworkCore;

public class AppointmentCalendar
{
    private readonly AppointmentRegistry appointmentRegistry;
    private readonly DoctorRegistry doctorRegistry;

    public AppointmentCalendar(AppointmentRegistry appointmentRegistry, DoctorRegistry doctorRegistry)
    {
        this.appointmentRegistry = appointmentRegistry;
        this.doctorRegistry = doctorRegistry;
    }

    public async Task SetDoctorSchedule(Schedule weeklySchedule, int doctorId)
    {
        var doctor = await this.doctorRegistry.GetDoctorWithId(doctorId);

        if (doctor is not null)
        {
            doctor.Schedule = weeklySchedule;
        }
    }

    public async Task<Schedule?> GetDoctorschedule(int doctorId)
    {
        var doctor = await this.doctorRegistry.GetDoctorWithId(doctorId);

        if (doctor is not null)
        {
            return doctor.Schedule;
        }
        else
        {
            return null;
        }
    }

    private async Task<IEnumerable<TimeSlot>> GetAvailableTimeSlots(int doctorId)
    {

    }
}
*/