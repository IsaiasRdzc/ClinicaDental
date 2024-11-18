namespace ClinicaDental.ApiService.Appointments.Services.MedicalRecords;

using System;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;
using ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;

using Microsoft.EntityFrameworkCore;

public class MedicalRecordsManager
{
    private readonly MedicalRecordsRegistry medicalRecordsRegistry;

    public MedicalRecordsManager(MedicalRecordsRegistry medicalRecordsRegistry)
    {
        this.medicalRecordsRegistry = medicalRecordsRegistry;
    }

    public async Task SaveMedicalRecord(int doctorId, MedicalRecord medicalRecord)
    {
        if (MedicalRecordsValidations.HasValidInformation(medicalRecord) && MedicalRecordsValidations.IsInAcceptableTime(medicalRecord)
        {
            await this.medicalRecordsRegistry.CreateMedicalRecord(medicalRecord);
        }
        else if (!MedicalRecordsValidations.IsInAcceptableTime(medicalRecord))
        {
            throw new ArgumentException("The time for modification has elapsed");
        }
        else
        {
            throw new ArgumentException("The data of the record is incorrect");
        }

    }

    public async Task DeleteMedicalRecordByRecordId(int doctorId, MedicalRecord medicalrecord)
    {
        if (!MedicalRecordsValidations.IsInAcceptableTime(medicalrecord))
        {
            throw new ArgumentException("The time for modification has elapsed");
        }

        await this.medicalRecordsRegistry.DeleteMedicalRecordByRecordId(medicalrecord.MedicalRecordId);

        // this will throw an exception if the Record its not found
    }

    public async Task UpdateMedicalRecord(int doctorId, MedicalRecord medicalrecord)
    {
        if (!MedicalRecordsValidations.IsInAcceptableTime(medicalrecord))
        {
            throw new ArgumentException("The time for modification has elapsed");
        }

        await this.medicalRecordsRegistry.UpdateMedicalRecord(medicalrecord);

        // this will throw an exception if the Record its not found
    }

    public IQueryable<MedicalRecord> SearchMedicalRecordsByDoctor(int doctorId)
    {
        return this.medicalRecordsRegistry.GetMedicalRecordsByDoctortId(doctorId);
    }

    public IQueryable<MedicalRecord> SearchMedicalRecordsByPatient(int doctorId, int patientId)
    {
        return this.medicalRecordsRegistry.GetMedicalRecordsByPatientId(patientId);
    }

    public IQueryable<MedicalRecord> SearchMedicalRecordByRecordId(int doctorId, int medicalRecordId)
    {
        var existingRrecords = this.medicalRecordsRegistry.GetMedicalRecordByMedicalRecordId(medicalRecordId);

        if (existingRrecords.FirstOrDefault() is null)
        {
            throw new KeyNotFoundException("Record not found");
        }
        else
        {
            return existingRrecords;
        }
    }
}
