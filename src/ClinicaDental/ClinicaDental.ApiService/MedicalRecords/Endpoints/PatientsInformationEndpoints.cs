namespace ClinicaDental.ApiService.MedicalRecords.Endpoints;

using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;
using ClinicaDental.ApiService.MedicalRecords.Services;

public static class PatientsInformationEndpoints
{
    public static void MapPatientInformationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/patientsInformation");
        group.MapPost("patient", CreatePatient);


        group.MapGet("PatientById", SearchPatientById);
        group.MapGet("PatientsByDoctorId", SearchPatientsByDoctorId);
       group.MapGet("Patients", SearchAllPatients);
        group.MapGet("PatientById/{patientId}", SearchPatientById);
        group.MapGet("PatientsByDoctorId/{doctorId}", SearchPatientsByDoctorId);


        group.MapPut("Patient", UpdatePatient);

        group.MapDelete("PatientById", DeletePatientById);
    }

    public static async Task<IResult> SearchAllPatients(PatientsInformationManager patientsInformationManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await patientsInformationManager.GetPatients());
    }

    public static async Task<IResult> CreatePatient(Patient patient, PatientsInformationManager patientsInformationManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await patientsInformationManager.SavePatient(patient));
    }

    public static async Task<IResult> UpdatePatient(int patientId, Patient patient, PatientsInformationManager patientsInformationManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await patientsInformationManager.UpdatePatientByPatientId(patientId, patient));
    }

    public static async Task<IResult> DeletePatientById(int patientId, PatientsInformationManager patientsInformationManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await patientsInformationManager.DeletePatientById(patientId));
    }

    public static async Task<IResult> SearchPatientById(int patientId, PatientsInformationManager patientsInformationManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await patientsInformationManager.SearchPatientById(patientId));
    }

    public static async Task<IResult> SearchPatientsByDoctorId(int doctorId, PatientsInformationManager patientsInformationManager)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await patientsInformationManager.SearchPatientByDoctorId(doctorId));
    }
}
