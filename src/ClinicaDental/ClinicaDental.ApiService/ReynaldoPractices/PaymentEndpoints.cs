namespace ClinicaDental.ApiService.ReynaldoPractices;
using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;
using ClinicaDental.ApiService.ReynaldoPractices.Services;
public static class PaymentEndpoints
{
    public static void MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/payment");

        //group.MapPost("initializeDoctor", CreateDoctorAccount);

        //group.MapGet("paymentDetail", GetPaymentDetails);
        group.MapGet("{id}", GetPaymentDetail);

        //group.MapPut("{id}", PutPaymentDetail);

        group.MapPost(string.Empty, PostPaymentDetail);

        //group.MapDelete("{id}", DeletePayment);

    }

    /*public static async Task<IResult> CreateDoctorAccount(Doctor doctor,ClinicAdmin clinicAdmin)
    {
        await clinicAdmin.CreateDoctorAccount(doctor);
        return Results.Ok();
    }*/

    /*public static async Task<IResult> GetPaymentDetails(){
        //await Console.WriteLine("hola a todos");
        //return Results.Ok();
    }*/

    public static async Task<IResult> GetPaymentDetail(
        int id,
        PaymentsAdmin admin)
    {
        var payment = await admin.GetPaymentDetailById(id);
        return Results.Ok(payment);
    }

    /*public static async Task<IResult> PutPaymentDetail(
        int id, PaymentDetail paymentDetail)
    {
        
    }*/

    public static async Task<IResult> PostPaymentDetail(
        PaymentDetail paymentDetail,
        PaymentsAdmin admin)
    {
        await admin.CreatePayment(paymentDetail);
        return Results.Ok();
    }

    /*
    public static async Task<IResult> DeletePayment(
        int id)
    {
        
    }*/
}
