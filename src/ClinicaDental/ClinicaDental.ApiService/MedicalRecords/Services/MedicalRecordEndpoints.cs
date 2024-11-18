namespace ClinicaDental.ApiService.Appointments.Services.MedicalRecords;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using Microsoft.EntityFrameworkCore;

public static class MedicalRecordEndpoints
{
    public static void MapMedicalRecordsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/medicalRecords");
        group.MapPost("newRecord", CreateRecord);

        group.MapGet("searchRecordById/{medicalRecordId}", SearchRecordById);
        group.MapGet("searchRecordsByDoctorId/{doctorId}", SearchRecordsByDoctorId);
        group.MapGet("searchRecordsByPatientId/{patientId}", SearchRecordsByPatientId);

        group.MapPut("updateRecord", UpdateRecord);

        group.MapDelete("deleteRecordById/{medicalRecordId}", DeleteMedicalRecordByRecordId);
    }

    public static async Task<IResult> CreateRecord(MedicalRecord medicalRecord, MedicalRecordsManager medicalRecordsManager)
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

    public static async Task<IResult> UpdateRecord(MedicalRecord medicalRecord, MedicalRecordsManager medicalRecordsManager)
    {
        try
        {
            await medicalRecordsManager.UpdateMedicalRecord(medicalRecord);
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

    public static async Task<IResult> DeleteMedicalRecordByRecordId(int medicalRecordId, MedicalRecordsManager medicalRecordsManager)
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

    public static async Task<IResult> SearchRecordById(int medicalRecordId, MedicalRecordsManager medicalRecordsManager)
    {
        try
        {
            var medicalRecords = await medicalRecordsManager.SearchMedicalRecordByRecordId(medicalRecordId).ToListAsync();
            return Results.Ok(medicalRecords);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
    }

    public static async Task<IResult> SearchRecordsByPatientId(int patientId, MedicalRecordsManager medicalRecordsManager)
    {
        try
        {
            var medicalRecords = await medicalRecordsManager.SearchMedicalRecordsByPatientId(patientId).ToListAsync();
            return Results.Ok(medicalRecords);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
    }

    public static async Task<IResult> SearchRecordsByDoctorId(int doctorId, MedicalRecordsManager medicalRecordsManager)
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
