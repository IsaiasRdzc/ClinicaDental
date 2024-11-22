namespace ClinicaDental.ApiService.DataBase.Models.Purchases;

using ClinicaDental.ApiService.DataBase.Models;

public class PurchaseType
{
    public int Id { get; set; } // Esto será el ID principal (1, 2, 3)
    public string Name { get; set; } = string.Empty; // Nombres: "Request", "Order", "Finalized"
}
