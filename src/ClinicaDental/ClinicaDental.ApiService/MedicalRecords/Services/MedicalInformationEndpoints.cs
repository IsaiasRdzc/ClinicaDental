namespace ClinicaDental.ApiService.MedicalRecords.Services;

using System.Runtime.CompilerServices;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using Microsoft.EntityFrameworkCore;

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
        try
        {
            await medicalRecordsManager.SaveMedicalRecord(medicalRecord);
            return Results.Ok();
        }
        catch (ArgumentException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> UpdateRecord(int medicalRecordId, MedicalRecord medicalRecord, MedicalInformationManager medicalRecordsManager)
    {
        try
        {
            await medicalRecordsManager.UpdateMedicalRecord(medicalRecordId, medicalRecord);
            return Results.Ok();
        }
        catch (ArgumentException error)
        {
            return Results.BadRequest(error.Message);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
    }

    public static async Task<IResult> DeleteMedicalRecordByRecordId(int medicalRecordId, MedicalInformationManager medicalRecordsManager)
    {
        try
        {
            await medicalRecordsManager.DeleteMedicalRecordByRecordId(medicalRecordId);
            return Results.Ok();
        }
        catch (ArgumentException error)
        {
            return Results.BadRequest(error.Message);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
    }

    public static async Task<IResult> SearchRecordById(int medicalRecordId, MedicalInformationManager medicalRecordsManager)
    {
        try
        {
            var medicalRecord = await medicalRecordsManager.SearchMedicalRecordByRecordId(medicalRecordId);
            return Results.Ok(medicalRecord);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
    }

    public static async Task<IResult> SearchRecordsByPatientId(int patientId, MedicalInformationManager medicalRecordsManager)
    {
        try
        {
            var medicalRecords = await medicalRecordsManager.SearchMedicalRecordsByPatientId(patientId).ToListAsync(); // remover
            return Results.Ok(medicalRecords);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
    }

    public static async Task<IResult> SearchRecordsByDoctorId(int doctorId, MedicalInformationManager medicalRecordsManager)
    {
        try
        {
            var medicalRecords = await medicalRecordsManager.SearchMedicalRecordsByDoctorId(doctorId).ToListAsync();
            return Results.Ok(medicalRecords);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
    }
}
