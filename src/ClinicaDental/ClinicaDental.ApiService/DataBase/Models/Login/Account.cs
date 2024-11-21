namespace ClinicaDental.ApiService.DataBase.Models.Login;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Account
{
    // Constructor for seeding purposes
    public Account(int id)
    {
        this.Id = id; // Set the ID for seeding
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public required int DoctorId { get; set; }

    public required string Username { get; set; } = null!;

    public required string Password { get; set; } = null!;
}
