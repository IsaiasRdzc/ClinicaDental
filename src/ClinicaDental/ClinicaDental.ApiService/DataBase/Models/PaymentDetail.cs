namespace ClinicaDental.ApiService.DataBase.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class PaymentDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int paymentDetailId { get; set; }

    public string cardOwnerName { get; set; } = "";

    public string cardNumber { get; set; }="";

    public string expirationDate { get; set; }="";

    public string securityCode { get; set; }="";
}
