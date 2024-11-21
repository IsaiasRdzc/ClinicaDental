namespace ClinicaDental.ApiService.MedicalRecords.Endpoints;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;
using ClinicaDental.ApiService.MedicalRecords.ErrorRelated;
using ClinicaDental.ApiService.MedicalRecords.Services;

public static class MedicalIRecordsEndpoints
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

    public static async Task<IResult> CreateRecord(MedicalRecord medicalRecord, MedicalRecordsManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.SaveMedicalRecord(medicalRecord));
    }

    public static async Task<IResult> UpdateRecordById(int medicalRecordId, MedicalRecord medicalRecord, MedicalRecordsManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.UpdateMedicalRecordById(medicalRecordId, medicalRecord));
    }

    public static async Task<IResult> DeleteMedicalRecordByRecordId(int medicalRecordId, MedicalRecordsManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.DeleteMedicalRecordByRecordId(medicalRecordId));
    }

    public static async Task<IResult> SearchRecordById(int medicalRecordId, MedicalRecordsManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.SearchMedicalRecordByRecordId(medicalRecordId));
    }

    public static async Task<IResult> SearchRecordsByPatientId(int patientId, MedicalRecordsManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.SearchMedicalRecordsByPatientId(patientId));
    }

    public static async Task<IResult> SearchRecordsByDoctorId(int doctorId, MedicalRecordsManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await medicalRecordsManager.SearchMedicalRecordsByDoctorId(doctorId));
    }
}
