namespace ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using Microsoft.EntityFrameworkCore;

public class MedicalRecordsRegistry(AppDbContext context)
{
    private readonly AppDbContext context = context;

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

    public async Task<MedicalRecord?> GetMedicalRecordByMedicalRecordId(int medicalRecordId)
    {
        var medicalRecord = await this.context.MedicalRecords
            .Where(medicalRecord => medicalRecord.MedicalRecordId == medicalRecordId)
            .Include(medicalRecord => medicalRecord.Diagnosis).ThenInclude(illness => illness.Treatments)
            .Include(medicalRecord => medicalRecord.MedicalProcedures)
            .Include(medicalRecord => medicalRecord.Teeths)
            .FirstOrDefaultAsync();

        return medicalRecord;
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

    public async Task DeleteMedicalRecordByRecordId(MedicalRecord existingMedicalRecord)
    {
        this.RemoveOldDiagnosisInfo(existingMedicalRecord);
        this.RemoveOldTeethInfo(existingMedicalRecord);
        this.RemoveOldMedicalProceduresInfo(existingMedicalRecord);

        this.context.Remove(existingMedicalRecord);
        await this.context.SaveChangesAsync();
    }

    public async Task UpdateMedicalRecord(MedicalRecord existingMedicalRecord, MedicalRecord updatedmedicalRecord)
    {
        this.RemoveOldDiagnosisInfo(existingMedicalRecord);
        this.RemoveOldTeethInfo(existingMedicalRecord);
        this.RemoveOldMedicalProceduresInfo(existingMedicalRecord);

        existingMedicalRecord.Diagnosis = updatedmedicalRecord.Diagnosis;
        existingMedicalRecord.Teeths = updatedmedicalRecord.Teeths;
        existingMedicalRecord.MedicalProcedures = updatedmedicalRecord.MedicalProcedures;

        this.context.Update(existingMedicalRecord);
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

    private void RemoveOldDiagnosisInfo(MedicalRecord existingMedicalRecord)
    {
        foreach (var diagnosis in existingMedicalRecord.Diagnosis)
        {
            this.context.Set<Medicine>().RemoveRange(diagnosis.Treatments);
        }

        this.context.Set<Illness>().RemoveRange(existingMedicalRecord.Diagnosis);
    }

    private void RemoveOldTeethInfo(MedicalRecord existingMedicalRecord)
    {
        this.context.Set<Teeth>().RemoveRange(existingMedicalRecord.Teeths);
    }

    private void RemoveOldMedicalProceduresInfo(MedicalRecord existingMedicalRecord)
    {
        this.context.Set<MedicalProcedure>().RemoveRange(existingMedicalRecord.MedicalProcedures);
    }
}
