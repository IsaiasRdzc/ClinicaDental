namespace ClinicaDental.ApiService.Login;

using ClinicaDental.ApiService.DataBase.Models.Login;
using ClinicaDental.ApiService.DataBase.Registries.Login;

using Microsoft.EntityFrameworkCore;

public class AccountsManager
{
    private readonly AccountsRegistry accountsRegistry;

    public AccountsManager(AccountsRegistry accountsRegistry)
    {
        this.accountsRegistry = accountsRegistry;
    }

    public async Task<bool> AttemptLogin(string username, string password)
    {
        var validAccounts = await this.accountsRegistry.GetAccountsList().ToListAsync();

        var userExists = validAccounts.Exists(account => account.Username == username
        && account.Password == password);

        if (userExists)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
