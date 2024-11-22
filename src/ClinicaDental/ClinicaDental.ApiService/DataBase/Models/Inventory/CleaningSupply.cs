﻿namespace ClinicaDental.ApiService.DataBase.Models.Inventory;
public class CleaningSupply : Supply
{
    public required string CleaningType { get; set; }

    public required string CleaningMethod { get; set; }

    public DateTime CleaningDate { get; set; }
}
