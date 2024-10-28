namespace ClinicaDental.ApiService.DataBase.Models;

public class Supply
{
    public int Id { get; set; } // Propiedad clave

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }
}
