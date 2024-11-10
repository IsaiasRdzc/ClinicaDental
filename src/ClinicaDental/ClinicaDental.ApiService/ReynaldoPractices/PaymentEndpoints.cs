namespace ClinicaDental.ApiService.ReynaldoPractices;
using ClinicaDental.ApiService.DataBase.Models;
public static class PaymentEndpoints
{
    public static void MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/payment");

        //group.MapPost("initializeDoctor", CreateDoctorAccount);

        group.MapGet("paymentDetail", GetPaymentDetails);
        group.MapGet("{id}", GetPaymentDetail);

        group.MapPut("{id}", PutPaymentDetail);

        group.MapPost(string.empty, PostPaymentDetail);

        group.MapDelete("{id}", DeletePayment);

    }

    /*public static async Task<IResult> CreateDoctorAccount(Doctor doctor,ClinicAdmin clinicAdmin)
    {
        await clinicAdmin.CreateDoctorAccount(doctor);
        return Results.Ok();
    }*/

    public static async Task<IResult> GetPaymentDetails(){
        //await Console.WriteLine("hola a todos");
        //return Results.Ok();
    }

    public static async Task<IResult> GetPaymentDetail(
        int id)
    {
        
    }

    public static async Task<IResult> PutPaymentDetail(
        int id, PaymentDetail paymentDetail)
    {
        
    }

    public static async Task<IResult> PostPaymentDetail(
        PaymentDetail paymentDetail)
    {
        
    }

    public static async Task<IResult> DeletePayment(
        int id)
    {
        
    }
}
