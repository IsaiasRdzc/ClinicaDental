namespace ClinicaDental.ApiService.MedicalRecords.Services;

using System;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;
using ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;

using Microsoft.EntityFrameworkCore;

public class MedicalInformationManager(MedicalRecordsRegistry medicalInformationRegistry)
{
    private readonly MedicalRecordsRegistry medicalInformationRegistry = medicalInformationRegistry;

    public async Task<int> SavePatient(Patient patient)
    {
        if (MedicalRecordInformationChecker.HasValidPatientInfo(patient))
        {
            return await this.medicalInformationRegistry.CreatePatient(patient);
        }
        else
        {
            throw new ArgumentException("The data of the record is incorrect");
        }
    }

    public async Task DeletePatientById(int patientId)
    {
        var existingPatient = await this.medicalInformationRegistry.GetPatientByPatientId(patientId);

        if (existingPatient is not null)
        {
            await this.medicalInformationRegistry.DeletePatient(existingPatient);
        }
        else
        {
            throw new KeyNotFoundException($"Patient with id {patientId} not found.");
        }
    }

    public async Task UpdatePatientByPatientId(int patientId, Patient updatedPatient)
    {
        var existingPatient = await this.medicalInformationRegistry.GetPatientByPatientId(patientId);

        if (existingPatient is not null && MedicalRecordInformationChecker.HasValidPatientInfo(updatedPatient))
        {
            await this.medicalInformationRegistry.UpdatePatient(existingPatient,updatedPatient);
        }
        else
        {
            throw new KeyNotFoundException($"Patient with id {patientId} not found.");
        }
    }

    public async Task<Patient> SearchPatientById(int patientId)
    {
        var existingPatient = await this.medicalInformationRegistry.GetPatientByPatientId(patientId);

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
        var existingPatients = this.medicalInformationRegistry.GetPatientsByDoctorId(doctorId);

        if (existingPatients.FirstOrDefault() is not null)
        {
            return existingPatients.ToListAsync();
        }
        else
        {
            throw new KeyNotFoundException("Record not found");
        }
    }

    public async Task SaveMedicalRecord(MedicalRecord medicalRecord)
    {
        if (MedicalRecordInformationChecker.HasValidMedicalRecordInformation(medicalRecord) && MedicalRecordInformationChecker.IsInAcceptableTime(medicalRecord.DateCreated))
        {
            await this.medicalInformationRegistry.CreateMedicalRecord(medicalRecord);
        }
        else if (!MedicalRecordInformationChecker.IsInAcceptableTime(medicalRecord.DateCreated))
        {
            throw new ArgumentException("The time for modification has elapsed");
        }
        else
        {
            throw new ArgumentException("The data of the record is incorrect");
        }
    }

    public async Task DeleteMedicalRecordByRecordId(int medicalRecordId)
    {
        var existingRecord = await this.medicalInformationRegistry.GetMedicalRecordByMedicalRecordId(medicalRecordId);

        if (existingRecord is not null)
        {
            if (!MedicalRecordInformationChecker.IsInAcceptableTime(existingRecord.DateCreated))
            {
                throw new ArgumentException("The time for modification has elapsed");
            }

            await this.medicalInformationRegistry.DeleteMedicalRecord(existingRecord);
        }
        else
        {
            throw new KeyNotFoundException($"MedicalRecord with id {medicalRecordId} not found.");
        }
    }

    public async Task UpdateMedicalRecordById(int medicalRecordId, MedicalRecord updatedmedicalRecord)
    {
        var existingRecord = await this.medicalInformationRegistry.GetMedicalRecordByMedicalRecordId(medicalRecordId);

        if (existingRecord is not null)
        {
            if (!MedicalRecordInformationChecker.IsInAcceptableTime(existingRecord.DateCreated))
            {
                throw new ArgumentException("The time for modification has elapsed");
            }

            await this.medicalInformationRegistry.UpdateMedicalRecord(existingRecord, updatedmedicalRecord);
        }
        else
        {
            throw new KeyNotFoundException($"MedicalRecord with id {medicalRecordId} not found.");
        }
    }

    public Task<List<MedicalRecord>> SearchMedicalRecordsByDoctorId(int doctorId)
    {
        var existingRrecords = this.medicalInformationRegistry.GetMedicalRecordsByDoctortId(doctorId);

        if (existingRrecords.FirstOrDefault() is not null)
        {
            return existingRrecords.ToListAsync();
        }
        else
        {
            throw new KeyNotFoundException("Record not found");
        }
    }

    public Task<List<MedicalRecord>> SearchMedicalRecordsByPatientId(int patientId)
    {
        var existingRrecords = this.medicalInformationRegistry.GetMedicalRecordsByPatientId(patientId);

        if (existingRrecords.FirstOrDefault() is not null)
        {
            return existingRrecords.ToListAsync();
        }
        else
        {
            throw new KeyNotFoundException("Record not found");
        }
    }

    public async Task<MedicalRecord> SearchMedicalRecordByRecordId(int medicalRecordId)
    {
        var existingRecord = await this.medicalInformationRegistry.GetMedicalRecordByMedicalRecordId(medicalRecordId);

        if (existingRecord is not null)
        {
            return existingRecord;
        }
        else
        {
            throw new KeyNotFoundException("Record not found");
        }
    }
}
