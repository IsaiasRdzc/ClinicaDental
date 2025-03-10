﻿namespace ClinicaDental.ApiService.MedicalRecords.Services;

using System;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;
using ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;

using Microsoft.EntityFrameworkCore;

public class MedicalRecordsManager(MedicalRecordsRegistry medicalRecordsRegistry, PatientsRegistry patientsRegistry)
{
    public async Task SaveMedicalRecord(MedicalRecord medicalRecord)
    {
        var existingPatient = await patientsRegistry.GetPatientByPatientId(medicalRecord.PatientId);

        if (MedicalRecordInformationChecker.HasValidMedicalRecordInformation(medicalRecord) && MedicalRecordInformationChecker.IsInAcceptableTime(medicalRecord.DateCreated) && existingPatient is not null)
        {
            await medicalRecordsRegistry.CreateMedicalRecord(medicalRecord);
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
        var existingRecord = await medicalRecordsRegistry.GetDetailedMedicalRecordByMedicalRecordId(medicalRecordId);

        if (existingRecord is not null)
        {
            if (!MedicalRecordInformationChecker.IsInAcceptableTime(existingRecord.DateCreated))
            {
                throw new ArgumentException("The time for modification has elapsed");
            }

            await medicalRecordsRegistry.DeleteMedicalRecord(existingRecord);
        }
        else
        {
            throw new KeyNotFoundException($"MedicalRecord with id {medicalRecordId} not found.");
        }
    }

    public async Task UpdateMedicalRecordById(int medicalRecordId, MedicalRecord updatedmedicalRecord)
    {
        var existingRecord = await medicalRecordsRegistry.GetDetailedMedicalRecordByMedicalRecordId(medicalRecordId);

        if (existingRecord is not null)
        {
            if (!MedicalRecordInformationChecker.IsInAcceptableTime(existingRecord.DateCreated))
            {
                throw new ArgumentException("The time for modification has elapsed");
            }

            await medicalRecordsRegistry.UpdateMedicalRecord(existingRecord, updatedmedicalRecord);
        }
        else
        {
            throw new KeyNotFoundException($"MedicalRecord with id {medicalRecordId} not found.");
        }
    }

    public Task<List<MedicalRecord>> SearchDetailedMedicalRecordsByDoctorId(int doctorId)
    {
        var existingRrecords = medicalRecordsRegistry.GetDetailedMedicalRecordsByDoctortId(doctorId);

        if (existingRrecords.FirstOrDefault() is not null)
        {
            return existingRrecords.ToListAsync();
        }
        else
        {
            throw new KeyNotFoundException("Record not found");
        }
    }

    public Task<List<MedicalRecord>> SearchMedicalRecordsByDoctorId(int doctorId)
    {
        var existingRrecords = medicalRecordsRegistry.GetMedicalRecordsByDoctortId(doctorId);

        if (existingRrecords.FirstOrDefault() is not null)
        {
            return existingRrecords.ToListAsync();
        }
        else
        {
            throw new KeyNotFoundException("Record not found");
        }
    }

    public Task<List<MedicalRecord>> SearchDetailedMedicalRecordsByPatientId(int patientId)
    {
        var existingRrecords = medicalRecordsRegistry.GetDetailedMedicalRecordsByPatientId(patientId);

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
        var existingRrecords = medicalRecordsRegistry.GetMedicalRecordsByPatientId(patientId);

        if (existingRrecords.FirstOrDefault() is not null)
        {
            return existingRrecords.ToListAsync();
        }
        else
        {
            throw new KeyNotFoundException("Record not found");
        }
    }

    public async Task<MedicalRecord> SearchDetailedMedicalRecordByRecordId(int medicalRecordId)
    {
        var existingRecord = await medicalRecordsRegistry.GetDetailedMedicalRecordByMedicalRecordId(medicalRecordId);

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
