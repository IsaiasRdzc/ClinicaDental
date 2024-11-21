namespace ClinicaDental.ApiService.MedicalRecords.Services;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

public static class MedicalInformationEndpoints
{
    public static void MapMedicalRecordsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/medicalRecords");
        group.MapPost("MedicalRecord", CreateRecord);

        group.MapGet("RecordById/{medicalRecordId}", SearchRecordById);
        group.MapGet("RecordsByDoctorId/{doctorId}", SearchRecordsByDoctorId);
        group.MapGet("RecordsByPatientId/{patientId}", SearchRecordsByPatientId);

        group.MapPut("Record/{medicalRecordId}", UpdateRecordById);

        group.MapDelete("RecordById/{medicalRecordId}", DeleteMedicalRecordByRecordId);
    }

    public static void MapPatientInformationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/patientsInformation");
        group.MapPost("patient", CreatePatient);

        group.MapGet("PatientById/{patienId}", SearchPatientById);
        group.MapGet("PatientsByDoctorId/{doctorId}", SearchPatientsByDoctorId);

        group.MapPut("Patient/{patientId}", UpdatePatient);

        group.MapDelete("PatientById/{patientId}", DeletePatientById);
    }

    public static async Task<IResult> CreatePatient(Patient patient, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.SavePatient(patient));
    }

    public static async Task<IResult> UpdatePatient(int patientId, Patient patient, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.UpdatePatientByPatientId(patientId, patient));
    }

    public static async Task<IResult> DeletePatientById(int patientId , MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.DeletePatientById(patientId));
    }

    public static async Task<IResult> SearchPatientById(int patientId, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.SearchPatientById(patientId));
    }

    public static async Task<IResult> SearchPatientsByDoctorId(int doctorId, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.SearchPatientByDoctorId(doctorId));
    }

    public static async Task<IResult> CreateRecord(MedicalRecord medicalRecord, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.SaveMedicalRecord(medicalRecord));
    }

    public static async Task<IResult> UpdateRecordById(int medicalRecordId, MedicalRecord medicalRecord, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.UpdateMedicalRecordById(medicalRecordId, medicalRecord));
    }

    public static async Task<IResult> DeleteMedicalRecordByRecordId(int medicalRecordId, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.DeleteMedicalRecordByRecordId(medicalRecordId));
    }

    public static async Task<IResult> SearchRecordById(int medicalRecordId, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.SearchMedicalRecordByRecordId(medicalRecordId));
    }

    public static async Task<IResult> SearchRecordsByPatientId(int patientId, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.SearchMedicalRecordsByPatientId(patientId));
    }

    public static async Task<IResult> SearchRecordsByDoctorId(int doctorId, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.SearchMedicalRecordsByDoctorId(doctorId));
    }
}
