namespace ClinicaDental.ApiService.ReynaldoPractices.Services;

using ClinicaDental.ApiService.DataBase.Models;
using ClinicaDental.ApiService.DataBase.Registries;
using Microsoft.EntityFrameworkCore;

public class PaymentsAdmin
{
    private readonly PaymentDetailRegistry paymentRegistry;
    
    public PaymentsAdmin(PaymentDetailRegistry paymentRegistry)
    {
        this.paymentRegistry = paymentRegistry;
    }

    public async Task<PaymentDetail?> GetPaymentDetailById(int id)
    {
        var payment = await this.paymentRegistry.GetPaymentDetailById(id);

        return payment;
    }

    public async Task CreatePayment(PaymentDetail paymentDetail)
    {
        await this.paymentRegistry.CreatePaymentDetail(paymentDetail);
    }

    public async Task<IEnumerable<PaymentDetail>> GetAllPaymentDetails()
    {
        var details = this.paymentRegistry.GetAllDetails();
        var allDetails = await details.ToListAsync();

        return allDetails;
    }
}