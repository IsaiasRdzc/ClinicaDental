namespace ClinicaDental.ApiService.Appointments.Services.MedicalRecords;

using ClinicaDental.ApiService.DataBase;

public static class AccessValidations
{
    public static bool DoctorExists(int doctorId, AppDbContext appContext)
    {
        return appContext.Doctors.Any(doctor => doctor.Id == doctorId);
    }

    public static bool HasAccessToFile(int recordDoctorId, int doctorId)
    {
        return recordDoctorId == doctorId;
    }
}
