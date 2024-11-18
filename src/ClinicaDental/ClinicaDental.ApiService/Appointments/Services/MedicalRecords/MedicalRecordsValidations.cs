namespace ClinicaDental.ApiService.Appointments.Services.MedicalRecords;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

public static class MedicalRecordsValidations
{
    private const int AcceptableTimeInHours = 3;

    public static bool HasTeethInfo(MedicalRecord medicalRecord)
    {
        return medicalRecord.Teeths.Count > 0;
    }

    public static bool IsInAcceptableTime(DateTime medicalRecordTime)
    {

        DateTime currentTime = DateTime.Now;
        TimeSpan maxDelay = TimeSpan.FromHours(AcceptableTimeInHours);
        TimeSpan difference = currentTime - medicalRecordTime;

        return difference <= maxDelay && medicalRecordTime <= currentTime;
    }

    public static bool HasValidPatientInfo(MedicalRecord medicalRecord)
    {
        return medicalRecord.Diagnosis.Count > 0 || medicalRecord.MedicalProcedures.Count > 0;
    }

    public static bool HasValidInformation(MedicalRecord medicalRecord)
    {
        return HasTeethInfo(medicalRecord) && HasValidPatientInfo(medicalRecord);
    }
}
