namespace ClinicaDental.ApiService.HumanResources;

using ClinicaDental.ApiService.Appointments.Services;
using ClinicaDental.ApiService.DataBase.Models.Appointments;
using ClinicaDental.ApiService.DataBase.Models.HumanResources;

public static class HREndpoints
{
    public static void MapHREndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/HR");

        group.MapPost("doctor", SetDoctorSchedule);
        group.MapPost("initializeDoctor", CreateDoctorAccount);

        group.MapGet("doctor", GetDoctorsList);
    }

    public static async Task<IResult> CreateDoctorAccount(
        Doctor doctor,
        PersonelAdmin personelAdmin)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await personelAdmin.CreateDoctorAccount(doctor));
    }

    public static async Task<IResult> SetDoctorSchedule(
        DoctorDaySchedule doctorSchedule,
        PersonelAdmin personelAdmin)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await personelAdmin.SetDoctorDaySchedule(doctorSchedule));
    }

    public static async Task<IResult> GetDoctorsList(
        PersonelAdmin personelAdmin)
    {
        return await ErrorOrResultHandler.HandleResult(async () => await personelAdmin.GetDoctorsList());
    }
}
