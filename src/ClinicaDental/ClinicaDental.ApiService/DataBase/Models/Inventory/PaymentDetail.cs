namespace ClinicaDental.ApiService.DataBase.Models.Inventory;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class PaymentDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentDetailId { get; set; }

    public string CardOwnerName { get; set; } = "";

    public string CardNumber { get; set; } = "";

    public string ExpirationDate { get; set; } = "";

    public string SecurityCode { get; set; } = "";
}
