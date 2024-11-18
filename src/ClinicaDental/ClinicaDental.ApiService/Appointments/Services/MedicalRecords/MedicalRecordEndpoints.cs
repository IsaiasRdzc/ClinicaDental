namespace ClinicaDental.ApiService.Appointments.Services.MedicalRecords;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using Microsoft.EntityFrameworkCore;

public static class MedicalRecordEndpoints
{
    public static void MapMedicalRecordEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/medicalRecords");
        group.MapPost("newRecord", CreateRecord);
        group.MapGet("searchRecord", SearchRecord);

    }

    public static async Task<IResult> CreateRecord(MedicalRecord medicalRecord, MedicalRecordsManager medicalRecordsManager)
    {
        try
        {
            await medicalRecordsManager.SaveMedicalRecord(1, medicalRecord);
            return Results.Ok();
        }
        catch (ArgumentException error)
        {
            return Results.BadRequest(error.Message);
        }
    }

    public static async Task<IResult> SearchRecord(int medicalRecordId, MedicalRecordsManager medicalRecordsManager)
    {
        try
        {
            var medicalRecords = await medicalRecordsManager.SearchMedicalRecordByRecordId(1, medicalRecordId).ToListAsync();
            return Results.Ok(medicalRecords);
        }
        catch (KeyNotFoundException error)
        {
            return Results.NotFound(error.Message);
        }
    }
}
