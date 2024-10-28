namespace ClinicaDental.ApiService.DataBase.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CleaningSupply : Supply
{
    public string CleaningAgent { get; set; }
}
