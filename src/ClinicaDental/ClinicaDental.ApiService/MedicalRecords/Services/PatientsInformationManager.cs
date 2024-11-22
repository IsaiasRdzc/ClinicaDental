namespace ClinicaDental.ApiService.MedicalRecords.Services;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;
using ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;

using Microsoft.EntityFrameworkCore;

public class PatientsInformationManager(PatientsRegistry patientsRegistry)
{
    public Task<List<Patient>> GetPatients()
    {
        var existingPatients = patientsRegistry.GetPatients();

        if (existingPatients.FirstOrDefault() is not null)
        {
            return existingPatients.ToListAsync();
        }
        else
        {
            throw new KeyNotFoundException("Record not found");
        }
    }

    public async Task<int> SavePatient(Patient patient)
    {
        if (MedicalRecordInformationChecker.HasValidPatientInfo(patient))
        {
            return await patientsRegistry.CreatePatient(patient);
        }
        else
        {
            throw new ArgumentException("The data of the record is incorrect");
        }
    }

    public async Task DeletePatientById(int patientId)
    {
        var existingPatient = await patientsRegistry.GetPatientByPatientId(patientId);

        if (existingPatient is not null)
        {
            await patientsRegistry.DeletePatient(existingPatient);
        }
        else
        {
            throw new KeyNotFoundException($"Patient with id {patientId} not found.");
        }
    }

    public async Task UpdatePatientByPatientId(int patientId, Patient updatedPatient)
    {
        var existingPatient = await patientsRegistry.GetPatientByPatientId(patientId);

        if (existingPatient is not null && MedicalRecordInformationChecker.HasValidPatientInfo(updatedPatient))
        {
            await patientsRegistry.UpdatePatient(existingPatient, updatedPatient);
        }
        else
        {
            throw new KeyNotFoundException($"Patient with id {patientId} not found.");
        }
    }

    public async Task<Patient> SearchPatientById(int patientId)
    {
        var existingPatient = await patientsRegistry.GetPatientByPatientId(patientId);

        if (existingPatient is not null)
        {
            return existingPatient;
        }
        else
        {
            throw new KeyNotFoundException("Record not found");
        }
    }

    public Task<List<Patient>> SearchPatientByDoctorId(int doctorId)
    {
        var existingPatients = patientsRegistry.GetPatientsByDoctorId(doctorId);

        if (existingPatients.FirstOrDefault() is not null)
        {
            return existingPatients.ToListAsync();
        }
        else
        {
            throw new KeyNotFoundException("Record not found");
        }
    }
}
