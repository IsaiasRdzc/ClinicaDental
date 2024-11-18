namespace ClinicaDental.ApiService.Appointments.Services.MedicalRecords;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

public static class MedicalRecordsValidations
{
    public static bool HasTeethInfo(MedicalRecord medicalRecord)
    {
        return medicalRecord.Teeths.Count > 0;
    }

    public static bool HasValidDate(MedicalRecord medicalRecord)
    {

        DateTime currentDate = DateTime.Now;
        TimeSpan maxDelay = TimeSpan.FromHours(3);
        TimeSpan difference = medicalRecord.DateCreated - currentDate;

        return difference <= maxDelay;
    }

    public static bool HasValidPatientInfo(MedicalRecord medicalRecord)
    {
        return medicalRecord.Diagnosis.Count > 0 || medicalRecord.MedicalProcedures.Count > 0;
    }

    public static bool HasValidInformation(MedicalRecord medicalRecord)
    {
        return MedicalRecordsValidations.HasTeethInfo(medicalRecord) && MedicalRecordsValidations.HasValidDate(medicalRecord)
            && MedicalRecordsValidations.HasValidPatientInfo(medicalRecord);
    }
}
