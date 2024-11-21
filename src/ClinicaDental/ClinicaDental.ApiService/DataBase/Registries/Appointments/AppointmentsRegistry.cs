namespace ClinicaDental.ApiService.DataBase.Registries;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models.Appointments;

public class AppointmentsRegistry(AppDbContext clinicDataBase)
{
    public IQueryable<Appointment> GetAppointmentsList()
    {
        var appointmentsList = clinicDataBase.AppointmentsTable.AsQueryable();
        return appointmentsList;
    }

    public IQueryable<Appointment> GetAppointmentsListByDate(DateOnly date)
    {
        var appointmentsList = clinicDataBase.AppointmentsTable
        .Where(appointment => appointment.Date == date)
        .AsQueryable();

        return appointmentsList;
    }

    public IQueryable<Appointment> GetAppointmentsListByDoctor(int doctorId)
    {
        var appointmentsList = clinicDataBase.AppointmentsTable
        .Where(appointment => appointment.DoctorId == doctorId)
        .AsQueryable();

        return appointmentsList;
    }

    public async Task<Appointment?> GetAppointmentByFolio(int folio)
    {
        var appointment = await clinicDataBase.AppointmentsTable.FindAsync(folio);
        return appointment;
    }

    public async Task CreateAppointment(Appointment appointment)
    {
        clinicDataBase.AppointmentsTable.Add(appointment);
        await clinicDataBase.SaveChangesAsync();
    }

    public async Task UpdateAppointment(int folio, DateOnly date, TimeOnly time, int duration)
    {
        var appointment = await this.GetAppointmentByFolio(folio);

        if (appointment is null)
        {
            throw new KeyNotFoundException($"Appointment with ID {folio} not found.");
        }

        appointment.Date = date;
        appointment.StartTime = time;
        appointment.DurationInHours = duration;

        clinicDataBase.AppointmentsTable.Update(appointment);
        await clinicDataBase.SaveChangesAsync();
    }

    public async Task DeleteAppointment(int folio)
    {
        var appointment = await this.GetAppointmentByFolio(folio);

        if (appointment is null)
        {
            throw new KeyNotFoundException($"Appointment with folio {folio} not found.");
        }

        clinicDataBase.AppointmentsTable.Remove(appointment);
        await clinicDataBase.SaveChangesAsync();
    }
}
