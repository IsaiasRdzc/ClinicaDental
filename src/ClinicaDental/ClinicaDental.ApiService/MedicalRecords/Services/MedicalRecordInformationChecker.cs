namespace ClinicaDental.ApiService.MedicalRecords.Services;

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

    public static bool HasValidMedicalInformation(MedicalRecord medicalRecord)
    {
        return HasTeethInfo(medicalRecord) && HasValidContentslInfo(medicalRecord);
    }

    private static bool HasTeethInfo(MedicalRecord medicalRecord)
    {
        return medicalRecord.Teeths.Count > 0;
    }

    private static bool HasValidContentslInfo(MedicalRecord medicalRecord)
    {
        return medicalRecord.Diagnosis.Count > 0 || medicalRecord.MedicalProcedures.Count > 0;
    }
}
