namespace ClinicaDental.ApiService.DataBase.Registries.Inventory;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models.Inventory;

public class PaymentDetailRegistry(AppDbContext clinicDataBase)
{
    public IQueryable<PaymentDetail> GetPaymentDetailList()
    {
        var paymentDetailsList = clinicDataBase.PaymentDetails.AsQueryable();
        return paymentDetailsList;
    }

    public async Task<PaymentDetail?> GetPaymentDetailById(int id)
    {
        var paymentDetail = await clinicDataBase.PaymentDetails.FindAsync(id);
        return paymentDetail;
    }

    public async Task CreatePaymentDetail(PaymentDetail paymentDetail)
    {
        clinicDataBase.PaymentDetails.Add(paymentDetail);
        await clinicDataBase.SaveChangesAsync();
    }

    public async Task DeletePaymentDetail(int id)
    {
        var paymentDetail = await this.GetPaymentDetailById(id);
        if (paymentDetail is null)
        {
            throw new KeyNotFoundException($"Payment with {id} not found.");
        }

        clinicDataBase.PaymentDetails.Remove(paymentDetail);
        await clinicDataBase.SaveChangesAsync();
    }

    public async Task UpdatePaymentDetail(int id, string name, string number, string expiration, string code)
    {
        var paymentDetail = await this.GetPaymentDetailById(id);
        if (paymentDetail is null)
        {
            throw new KeyNotFoundException($"Payment with {id} not found.");
        }

        paymentDetail.PaymentDetailId = id;
        paymentDetail.CardOwnerName = name;
        paymentDetail.CardNumber = number;
        paymentDetail.ExpirationDate = expiration;
        paymentDetail.SecurityCode = code;

        clinicDataBase.PaymentDetails.Update(paymentDetail);
        await clinicDataBase.SaveChangesAsync();
    }

    public IQueryable<PaymentDetail> GetAllDetails()
    {
        var details = clinicDataBase.PaymentDetails
        .AsQueryable();
        return details;
    }
}
