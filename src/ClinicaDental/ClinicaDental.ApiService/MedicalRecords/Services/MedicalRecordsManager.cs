namespace ClinicaDental.ApiService.MedicalRecords.Services;

using System;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;
using ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;

public class MedicalRecordsManager
{
    private readonly MedicalRecordsRegistry medicalRecordsRegistry;

    public MedicalRecordsManager(MedicalRecordsRegistry medicalRecordsRegistry)
    {
        this.medicalRecordsRegistry = medicalRecordsRegistry;
    }

    public async Task SaveMedicalRecord(MedicalRecord medicalRecord)
    {
        if (MedicalRecordsValidations.HasValidInformation(medicalRecord) && MedicalRecordsValidations.IsInAcceptableTime(medicalRecord.DateCreated))
        {
            await this.medicalRecordsRegistry.CreateMedicalRecord(medicalRecord);
        }
        else if (!MedicalRecordsValidations.IsInAcceptableTime(medicalRecord.DateCreated))
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
            if (!MedicalRecordsValidations.IsInAcceptableTime(existingRecord.DateCreated))
            {
                throw new ArgumentException("The time for modification has elapsed");
            }

            await this.medicalRecordsRegistry.DeleteMedicalRecordByRecordId(existingRecord.MedicalRecordId);
        }
        else
        {
            throw new KeyNotFoundException($"MedicalRecord with id {medicalRecordId} not found.");
        }
    }

    public async Task UpdateMedicalRecord(MedicalRecord medicalRecord)
    {
        var existingRecord = await this.medicalRecordsRegistry.GetMedicalRecordByMedicalRecordId(medicalRecord.MedicalRecordId);

        if (existingRecord is not null)
        {
            if (!MedicalRecordsValidations.IsInAcceptableTime(existingRecord.DateCreated))
            {
                throw new ArgumentException("The time for modification has elapsed");
            }

            await this.medicalRecordsRegistry.UpdateMedicalRecord(medicalRecord);
        }
        else
        {
            throw new KeyNotFoundException($"MedicalRecord with id {medicalRecord.MedicalRecordId} not found.");
        }
    }

    public IQueryable<MedicalRecord> SearchMedicalRecordsByDoctorId(int doctorId)
    {
        var existingRrecords = this.medicalRecordsRegistry.GetMedicalRecordsByDoctortId(doctorId);

        if (existingRrecords.FirstOrDefault() is not null)
        {
            return existingRrecords;
        }
        else
        {
            throw new KeyNotFoundException("Record not found");
        };
    }

    public IQueryable<MedicalRecord> SearchMedicalRecordsByPatientId(int patientId)
    {
        var existingRrecords = this.medicalRecordsRegistry.GetMedicalRecordsByPatientId(patientId);

        if (existingRrecords.FirstOrDefault() is not null)
        {
            return existingRrecords;
        }
        else
        {
            throw new KeyNotFoundException("Record not found");
        }
    }

    public async Task<MedicalRecord?> SearchMedicalRecordByRecordId(int medicalRecordId)
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
