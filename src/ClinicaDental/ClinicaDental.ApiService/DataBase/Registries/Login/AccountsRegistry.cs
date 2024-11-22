namespace ClinicaDental.ApiService.DataBase.Registries.Login;

using ClinicaDental.ApiService.DataBase;
using ClinicaDental.ApiService.DataBase.Models.Appointments;
using ClinicaDental.ApiService.DataBase.Models.Login;

public class AccountsRegistry(AppDbContext clinicDataBase)
{
    public IQueryable<Account> GetAccountsList()
    {
        var accountsList = clinicDataBase.AccountsTable.AsQueryable();
        return accountsList;
    }
}
