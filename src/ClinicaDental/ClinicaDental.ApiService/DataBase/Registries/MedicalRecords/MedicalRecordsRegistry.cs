namespace ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using Microsoft.EntityFrameworkCore;

public class MedicalRecordsRegistry
{
    private readonly AppDbContext context;

    public MedicalRecordsRegistry(AppDbContext context)
    {
        this.context = context;
    }

    public IQueryable<MedicalRecord> GetMedicalRecords()
    {
        return this.context.MedicalRecords.AsQueryable();
    }

    public IQueryable<MedicalRecord> GetMedicalRecordsByPatientId(int patientId)
    {
        var medicalRecords = this.context.MedicalRecords
        .AsQueryable()
        .Where(medicalRecord => medicalRecord.PatientId == patientId)
            .Include(medicalRecord => medicalRecord.Diagnosis).ThenInclude(illness => illness.Treatments)
            .Include(medicalRecord => medicalRecord.MedicalProcedures)
            .Include(medicalRecord => medicalRecord.Teeths);

        return medicalRecords;
    }

    public IQueryable<MedicalRecord> GetMedicalRecordByMedicalRecordId(int medicalRecordId)
    {
        var medicalRecords = this.context.MedicalRecords
        .AsQueryable()
        .Where(medicalRecord => medicalRecord.MedicalRecordId == medicalRecordId)
            .Include(medicalRecord => medicalRecord.Diagnosis).ThenInclude(illness => illness.Treatments)
            .Include(medicalRecord => medicalRecord.MedicalProcedures)
            .Include(medicalRecord => medicalRecord.Teeths);

        return medicalRecords;
    }

    public IQueryable<MedicalRecord> GetMedicalRecordsByDoctortId(int doctorId)
    {
        var medicalRecords = this.context.MedicalRecords
        .AsQueryable()
        .Where(medicalRecord => medicalRecord.PatientId == doctorId)
            .Include(medicalRecord => medicalRecord.Diagnosis).ThenInclude(illness => illness.Treatments)
            .Include(medicalRecord => medicalRecord.MedicalProcedures)
            .Include(medicalRecord => medicalRecord.Teeths); ;

        return medicalRecords;
    }

    public async Task CreateMedicalRecord(MedicalRecord medicalRecord)
    {
        if (medicalRecord is not null)
        {
            this.context.MedicalRecords.Add(medicalRecord);
            await this.context.SaveChangesAsync();
        }
    }

    public async Task DeleteMedicalRecordByRecordId(int medicalRecordId)
    {
        var existingMedicalRecord = this.GetMedicalRecordByMedicalRecordId(medicalRecordId);
        this.context.Remove(existingMedicalRecord);
        await this.context.SaveChangesAsync();
    }

    public async Task UpdateMedicalRecord(MedicalRecord medicalRecord)
    {
        this.context.MedicalRecords.Update(medicalRecord);
        await this.context.SaveChangesAsync();
    }

    public IQueryable<MedicalRecord> GetMedicalRecordsByPatientIdWithinTimeSpan(int patientId,DateTime startDate, DateTime finalDate)
    {
        var medicalRecords = this.GetMedicalRecordsByPatientId(patientId)
            .Include(medicalRecord => medicalRecord.Diagnosis).ThenInclude(illness => illness.Treatments)
            .Include(medicalRecord => medicalRecord.MedicalProcedures)
            .Include(medicalRecord => medicalRecord.Teeths);

        return medicalRecords.Where(medicalRecord => medicalRecord.DateCreated >= startDate && medicalRecord.DateCreated <= finalDate);
    }

    public IQueryable<MedicalRecord> GetMedicalRecordsByDoctorIdWithinTimeSpan(int patientId, DateTime startDate, DateTime finalDate)
    {
        var medicalRecords = this.GetMedicalRecordsByDoctortId(patientId)
            .Include(medicalRecord => medicalRecord.Diagnosis).ThenInclude(illness => illness.Treatments)
            .Include(medicalRecord => medicalRecord.MedicalProcedures)
            .Include(medicalRecord => medicalRecord.Teeths);

        return medicalRecords.Where(medicalRecord => medicalRecord.DateCreated >= startDate && medicalRecord.DateCreated <= finalDate);
    }
}
