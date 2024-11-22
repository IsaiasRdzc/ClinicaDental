namespace ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using Microsoft.EntityFrameworkCore;

public class MedicalRecordsRegistry(AppDbContext database)
{
    public IQueryable<MedicalRecord> GetMedicalRecords()
    {
        return database.MedicalRecords.AsQueryable();
    }

    public IQueryable<MedicalRecord> GetMedicalRecordsByPatientId(int patientId)
    {
        var medicalRecords = database.MedicalRecords
        .AsQueryable()
        .Where(medicalRecord => medicalRecord.PatientId == patientId);

        return medicalRecords;
    }

    public IQueryable<MedicalRecord> GetDetailedMedicalRecordsByPatientId(int patientId)
    {
        var medicalRecords = database.MedicalRecords
        .AsQueryable()
        .Where(medicalRecord => medicalRecord.PatientId == patientId)
            .Include(medicalRecord => medicalRecord.Diagnosis).ThenInclude(illness => illness.Treatments)
            .Include(medicalRecord => medicalRecord.MedicalProcedures)
            .Include(medicalRecord => medicalRecord.Teeths);

        return medicalRecords;
    }

    public async Task<MedicalRecord?> GetDetailedMedicalRecordByMedicalRecordId(int medicalRecordId)
    {
        var medicalRecord = await database.MedicalRecords
            .Where(medicalRecord => medicalRecord.MedicalRecordId == medicalRecordId)
            .Include(medicalRecord => medicalRecord.Diagnosis).ThenInclude(illness => illness.Treatments)
            .Include(medicalRecord => medicalRecord.MedicalProcedures)
            .Include(medicalRecord => medicalRecord.Teeths)
            .FirstOrDefaultAsync();

        return medicalRecord;
    }

    public IQueryable<MedicalRecord> GetMedicalRecordsByDoctortId(int doctorId)
    {
        var medicalRecords = database.MedicalRecords
        .AsQueryable()
        .Where(medicalRecord => medicalRecord.DoctorId == doctorId);

        return medicalRecords;
    }

    public IQueryable<MedicalRecord> GetDetailedMedicalRecordsByDoctortId(int doctorId)
    {
        var medicalRecords = database.MedicalRecords
        .AsQueryable()
        .Where(medicalRecord => medicalRecord.DoctorId == doctorId)
            .Include(medicalRecord => medicalRecord.Diagnosis).ThenInclude(illness => illness.Treatments)
            .Include(medicalRecord => medicalRecord.MedicalProcedures)
            .Include(medicalRecord => medicalRecord.Teeths);

        return medicalRecords;
    }

    public async Task CreateMedicalRecord(MedicalRecord medicalRecord)
    {
        database.MedicalRecords.Add(medicalRecord);
        await database.SaveChangesAsync();
    }

    public async Task DeleteMedicalRecord(MedicalRecord existingMedicalRecord)
    {
        this.RemoveOldDiagnosisInfo(existingMedicalRecord);
        this.RemoveOldTeethInfo(existingMedicalRecord);
        this.RemoveOldMedicalProceduresInfo(existingMedicalRecord);

        database.MedicalRecords.Remove(existingMedicalRecord);
        await database.SaveChangesAsync();
    }

    public async Task UpdateMedicalRecord(MedicalRecord existingMedicalRecord, MedicalRecord updatedmedicalRecord)
    {
        this.RemoveOldDiagnosisInfo(existingMedicalRecord);
        this.RemoveOldTeethInfo(existingMedicalRecord);
        this.RemoveOldMedicalProceduresInfo(existingMedicalRecord);

        existingMedicalRecord.Diagnosis = updatedmedicalRecord.Diagnosis;
        existingMedicalRecord.Teeths = updatedmedicalRecord.Teeths;
        existingMedicalRecord.MedicalProcedures = updatedmedicalRecord.MedicalProcedures;

        database.MedicalRecords.Update(existingMedicalRecord);
        await database.SaveChangesAsync();
    }

    private void RemoveOldDiagnosisInfo(MedicalRecord existingMedicalRecord)
    {
        foreach (var diagnosis in existingMedicalRecord.Diagnosis)
        {
            database.Set<Medicine>().RemoveRange(diagnosis.Treatments);
        }

        database.Set<Illness>().RemoveRange(existingMedicalRecord.Diagnosis);
    }

    private void RemoveOldTeethInfo(MedicalRecord existingMedicalRecord)
    {
        database.Set<Teeth>().RemoveRange(existingMedicalRecord.Teeths);
    }

    private void RemoveOldMedicalProceduresInfo(MedicalRecord existingMedicalRecord)
    {
        database.Set<MedicalProcedure>().RemoveRange(existingMedicalRecord.MedicalProcedures);
    }
}
