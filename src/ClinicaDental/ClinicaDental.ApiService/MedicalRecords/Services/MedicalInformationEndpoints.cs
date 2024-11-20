﻿namespace ClinicaDental.ApiService.MedicalRecords.Services;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

public static class MedicalInformationEndpoints
{
    public static void MapMedicalRecordsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/medicalRecords");
        group.MapPost("newMedicalRecord", CreateRecord);

        group.MapGet("searchRecordById/{medicalRecordId}", SearchRecordById);
        group.MapGet("searchRecordsByDoctorId/{doctorId}", SearchRecordsByDoctorId);
        group.MapGet("searchRecordsByPatientId/{patientId}", SearchRecordsByPatientId);

        group.MapPut("updateRecord/{medicalRecordId}", UpdateRecord);

        group.MapDelete("deleteRecordById/{medicalRecordId}", DeleteMedicalRecordByRecordId);
    }

    public static async Task<IResult> CreateRecord(MedicalRecord medicalRecord, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleRequest(async () => await medicalRecordsManager.SaveMedicalRecord(medicalRecord));
    }

    public static async Task<IResult> UpdateRecord(int medicalRecordId, MedicalRecord medicalRecord, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleRequest(async () => await medicalRecordsManager.UpdateMedicalRecord(medicalRecordId, medicalRecord));
    }

    public static async Task<IResult> DeleteMedicalRecordByRecordId(int medicalRecordId, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleRequest(async () => await medicalRecordsManager.DeleteMedicalRecordByRecordId(medicalRecordId));
    }

    public static async Task<IResult> SearchRecordById(int medicalRecordId, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleRequest(async () => await medicalRecordsManager.SearchMedicalRecordByRecordId(medicalRecordId));
    }

    public static async Task<IResult> SearchRecordsByPatientId(int patientId, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleRequest(async () => await medicalRecordsManager.SearchMedicalRecordsByPatientId(patientId));
    }

    public static async Task<IResult> SearchRecordsByDoctorId(int doctorId, MedicalInformationManager medicalRecordsManager)
    {
        return await ErrorOrResultHandler.HandleRequest(async () => await medicalRecordsManager.SearchMedicalRecordsByDoctorId(doctorId));
    }
}
