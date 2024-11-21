namespace ClinicaDental.ApiService.MedicalRecords.Services;

using System;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;
using ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;

using Microsoft.EntityFrameworkCore;

public class MedicalInformationManager(MedicalRecordsRegistry medicalRecordsRegistry)
{
    private readonly MedicalRecordsRegistry medicalRecordsRegistry = medicalRecordsRegistry;

    public async Task SaveMedicalRecord(MedicalRecord medicalRecord)
    {
        if (MedicalRecordInformationChecker.HasValidMedicalInformation(medicalRecord) && MedicalRecordInformationChecker.IsInAcceptableTime(medicalRecord.DateCreated))
        {
            await this.medicalRecordsRegistry.CreateMedicalRecord(medicalRecord);
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
        var existingRecord = await this.medicalRecordsRegistry.GetMedicalRecordByMedicalRecordId(medicalRecordId);

        if (existingRecord is not null)
        {
            if (!MedicalRecordInformationChecker.IsInAcceptableTime(existingRecord.DateCreated))
            {
                throw new ArgumentException("The time for modification has elapsed");
            }

            await this.medicalRecordsRegistry.DeleteMedicalRecordByRecordId(existingRecord);
        }
        else
        {
            throw new KeyNotFoundException($"MedicalRecord with id {medicalRecordId} not found.");
        }
    }

    public async Task UpdateMedicalRecord(int medicalRecordId, MedicalRecord updatedmedicalRecord)
    {
        var existingRecord = await this.medicalRecordsRegistry.GetMedicalRecordByMedicalRecordId(medicalRecordId);

        if (existingRecord is not null)
        {
            if (!MedicalRecordInformationChecker.IsInAcceptableTime(existingRecord.DateCreated))
            {
                throw new ArgumentException("The time for modification has elapsed");
            }

            await this.medicalRecordsRegistry.UpdateMedicalRecord(existingRecord, updatedmedicalRecord);
        }
        else
        {
            throw new KeyNotFoundException($"MedicalRecord with id {medicalRecordId} not found.");
        }
    }

    public Task<List<MedicalRecord>> SearchMedicalRecordsByDoctorId(int doctorId)
    {
        var existingRrecords = this.medicalRecordsRegistry.GetMedicalRecordsByDoctortId(doctorId);

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
        var existingRrecords = this.medicalRecordsRegistry.GetMedicalRecordsByPatientId(patientId);

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
        var existingRecord = await this.medicalRecordsRegistry.GetMedicalRecordByMedicalRecordId(medicalRecordId);

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
