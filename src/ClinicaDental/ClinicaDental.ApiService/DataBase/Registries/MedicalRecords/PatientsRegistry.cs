namespace ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using Microsoft.EntityFrameworkCore;

public class PatientsRegistry(AppDbContext database)
{
    public IQueryable<Patient> GetPatients()
    {
        return database.Patients.AsQueryable();
    }

    public async Task<Patient?> GetPatientByPatientId(int patientId)
    {
        var patient = await database.Patients
            .Where(patient => patient.PatientId == patientId)
            .FirstOrDefaultAsync();

        return patient;
    }

    public IQueryable<Patient> GetPatientsByDoctorId(int doctorId)
    {
        var patients = database.Patients
        .AsQueryable()
        .Where(patient => patient.DoctorId == doctorId);

        return patients;
    }

    public async Task<int> CreatePatient(Patient patient)
    {
        database.Patients.Add(patient);
        await database.SaveChangesAsync();
        return patient.PatientId;
    }

    public async Task DeletePatient(Patient patient)
    {
        database.Patients.Remove(patient);
        await database.SaveChangesAsync();
    }

    public async Task UpdatePatient(Patient existingPatient, Patient newPatient)
    {
        existingPatient.PatientAge = newPatient.PatientAge;
        existingPatient.PatientDirection = newPatient.PatientDirection;
        existingPatient.DoctorId = newPatient.DoctorId;
        existingPatient.PatientPhoneNumber = newPatient.PatientPhoneNumber;
        existingPatient.PatientEmail = newPatient.PatientEmail;

        database.Patients.Update(existingPatient);
        await database.SaveChangesAsync();
    }
}
