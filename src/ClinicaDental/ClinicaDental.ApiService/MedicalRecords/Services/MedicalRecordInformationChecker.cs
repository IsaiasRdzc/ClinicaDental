﻿namespace ClinicaDental.ApiService.MedicalRecords.Services;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

public static class MedicalRecordInformationChecker
{
    private const int MaxHourIntervalForModification = 3;

    public static bool IsInAcceptableTime(DateTime medicalRecordTime)
    {
        DateTime currentTime = DateTime.Now;
        var maxDelay = TimeSpan.FromHours(MaxHourIntervalForModification);
        TimeSpan difference = currentTime - medicalRecordTime;

        return difference <= maxDelay && medicalRecordTime <= currentTime;
    }

    public static bool HasValidMedicalRecordInformation(MedicalRecord medicalRecord)
    {
        if (medicalRecord == null)
        {
            return false;
        }

        return HasTeethInfo(medicalRecord) && HasValidContentslInfo(medicalRecord);
    }

    public static bool HasValidPatientInfo(Patient patient)
    {
        if (patient == null)
        {
            return false;
        }

        return IsPatientEmptyStrings(patient) && IsPatientWhiteSpace(patient) && HasValidAge(patient);
    }

    private static bool HasTeethInfo(MedicalRecord medicalRecord)
    {
        return medicalRecord.Teeths.Count > 0;
    }

    private static bool HasValidContentslInfo(MedicalRecord medicalRecord)
    {
        return medicalRecord.Diagnosis.Count > 0 || medicalRecord.MedicalProcedures.Count > 0;
    }

    private static bool IsPatientEmptyStrings(Patient patient)
    {
        return !(string.IsNullOrEmpty(patient.PatientNames)
            && string.IsNullOrEmpty(patient.PatientSecondNames)
            && string.IsNullOrEmpty(patient.PatientDirection)
            && string.IsNullOrEmpty(patient.PatientPhoneNumber));
    }

    private static bool IsPatientWhiteSpace(Patient patient)
    {
        return !(string.IsNullOrWhiteSpace(patient.PatientNames)
            && string.IsNullOrWhiteSpace(patient.PatientSecondNames)
            && string.IsNullOrWhiteSpace(patient.PatientDirection)
            && string.IsNullOrWhiteSpace(patient.PatientPhoneNumber));
    }

    private static bool HasValidAge(Patient patient){
        return patient.PatientAge > 0 && patient.PatientAge < 100;
    }
}
