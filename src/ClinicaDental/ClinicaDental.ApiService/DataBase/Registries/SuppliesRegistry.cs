namespace ClinicaDental.ApiService.DataBase.Registries;

using System.ComponentModel.DataAnnotations;
using System.Linq;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models;

using Microsoft.EntityFrameworkCore;

public class SuppliesRegistry(AppDbContext context)
{
    public void AddSupply(Supply supply)
    {
        context.Supplies.Add(supply);
    }

    public void RemoveSupply(Supply supply)
    {
        context.Supplies.Remove(supply);
    }

    public void UpdateSupply(Supply supply)
    {
        context.Supplies.Update(supply);
    }

    public IQueryable<Supply> GetSupplies()
    {
        return context.Supplies.AsQueryable().AsNoTracking();
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

}
