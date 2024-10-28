namespace ClinicaDental.ApiService.DataBase.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SurgicalSupply : Supply
{
    public bool IsSterile { get; set; }
}
