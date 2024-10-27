namespace ClinicaDental.ApiService.DataBase;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Appointment
{
    [Key]
    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("Date")]
    public string? Date { get; set; }

    [JsonPropertyName("PatientName")]
    public string? PatientName { get; set; }

    [JsonPropertyName("DrName")]
    public decimal DrName { get; set; }
}