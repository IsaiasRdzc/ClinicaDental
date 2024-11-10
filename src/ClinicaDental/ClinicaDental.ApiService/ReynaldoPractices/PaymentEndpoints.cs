namespace ClinicaDental.ApiService.ReynaldoPractices;

public static class PaymentEndpoints
{
    public static void MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/payment");
    }
}
