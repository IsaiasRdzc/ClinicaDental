namespace ClinicaDental.ApiService.DataBase.Registries;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService,DataBase.Models;

public class PaymentDetailRegistry
{
    private readonly AppDbContext context;

    public PaymentDetailRegistry(AppDbContext context)
    {
        this.context =  context;
    }

    public IQueryable<PaymentDetail> GetPaymentDetailList()
    {
        var paymentDetailsList = this.context.PaymentDetail.AsQueryable();
        return paymentDetailsList;
    }

    public async Task<PaymentDetail?> GetPaymentDetailById (int id)
    {
        var paymentDetail = await this.context.PaymentDetails.FindAsync(id);
        return paymentDetail;
    }

    public async Task CreatePaymentDetail (PaymentDetail paymentDetail){
        this.context.PaymentDetails.Add(paymentDetail);+
        await this.context.SaveChangesAsync();
    }

    public async Task DeletePaymentDetail(int id)
    {
        var paymentDetail = await this.GetPaymentDetailById(id);
        if (paymentDetail is null)
        {
            throw new KeyNotFoundException($"Payment with {id} not found.");
        }

        this.context.PaymentDetails.Remove(paymentDetail);
        await this.context.SaveChangesAsync();
    }

    public async Task UpdatePaymentDetail(int id, string name, string number, string expiration, string code)
    {
        var paymentDetail = await this.GetPaymentDetailById(id);
        if(paymentDetail is null){
            throw new KeyNotFoundException($"Payment with {id} not found.");
        }

        paymentDetail.id = id;
        paymentDetail.cardOwnerName = name;
        paymentDetail.cardNumber = number;
        paymentDetail.expirationDate = expiration;
        paymentDetail.securityCode = code;

        this.context.PaymentDetails.Update(paymentDetail);
        await this.context.SaveChangesAsync();

    }
}