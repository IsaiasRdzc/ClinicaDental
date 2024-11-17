namespace ClinicaDental.ApiService.DataBase.Registries;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;

public class PaymentDetailRegistry
{
    private readonly AppDbContext context;

    public PaymentDetailRegistry(AppDbContext context)
    {
        this.context =  context;
    }

    public IQueryable<PaymentDetail> GetPaymentDetailList()
    {
        var paymentDetailsList = this.context.PaymentDetails.AsQueryable();
        return paymentDetailsList;
    }

    public async Task<PaymentDetail?> GetPaymentDetailById (int id)
    {
        var paymentDetail = await this.context.PaymentDetails.FindAsync(id);
        return paymentDetail;
    }

    public async Task CreatePaymentDetail (PaymentDetail paymentDetail){
        this.context.PaymentDetails.Add(paymentDetail);
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

        paymentDetail.PaymentDetailId = id;
        paymentDetail.CardOwnerName = name;
        paymentDetail.CardNumber = number;
        paymentDetail.ExpirationDate = expiration;
        paymentDetail.SecurityCode = code;

        this.context.PaymentDetails.Update(paymentDetail);
        await this.context.SaveChangesAsync();

    }

    public IQueryable<PaymentDetail> GetAllDetails(){
        var details = this.context.PaymentDetails
        .AsQueryable();
        
        return details;
    }
}