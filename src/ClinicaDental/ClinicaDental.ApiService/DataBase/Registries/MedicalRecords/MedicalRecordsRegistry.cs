namespace ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

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
        .Where(medicalRecord => medicalRecord.PatientId == patientId);

        return medicalRecords;
    }

    public IQueryable<MedicalRecord> GetMedicalRecordsByDoctortId(int doctorId)
    {
        var medicalRecords = this.context.MedicalRecords
        .AsQueryable()
        .Where(medicalRecord => medicalRecord.PatientId == doctorId);

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

    public async Task DeleteMedicalRecord(int medicalRecordId)
    {
        var medicalRecord = this.GetMedicalRecordsByPatientId(medicalRecordId);

        if (medicalRecord is not null)
        {
            this.context.Remove(medicalRecord);
        }
        else
        {
            throw new KeyNotFoundException($"MedicalRecord with id {medicalRecord} not found.");
        }

        await this.context.SaveChangesAsync();
    }
}
