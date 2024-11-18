namespace ClinicaDental.ApiService.Appointments.Services.MedicalRecords;

using System;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;
using ClinicaDental.ApiService.DataBase.Registries.MedicalRecords;

public class MedicalRecordsManager
{
    private readonly MedicalRecordsRegistry medicalRecordsRegistry;

    public MedicalRecordsManager(MedicalRecordsRegistry medicalRecordsRegistry)
    {
        this.medicalRecordsRegistry = medicalRecordsRegistry;
    }

    public async Task SaveMeedicalRecord(MedicalRecord medicalRecord)
    {
        if (!MedicalRecordsValidations.HasValidInformation(medicalRecord))
        {
            throw new ArgumentException("The Record has no real usefull data");
        }

        await this.medicalRecordsRegistry.CreateMedicalRecord(medicalRecord);
    }
}
