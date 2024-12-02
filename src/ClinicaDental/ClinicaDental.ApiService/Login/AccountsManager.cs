namespace ClinicaDental.ApiService.Login;

using System.Data;

using ClinicaDental.ApiService.DataBase.Models.HumanResources;
using ClinicaDental.ApiService.DataBase.Registries.HumanResources;
using ClinicaDental.ApiService.DataBase.Registries.Login;

using Microsoft.EntityFrameworkCore;

public class AccountsManager
{
    private readonly AccountsRegistry accountsRegistry;
    private readonly DoctorsRegistry doctorsRegistry;

    public AccountsManager(AccountsRegistry accountsRegistry, DoctorsRegistry doctorsRegistry)
    {
        this.accountsRegistry = accountsRegistry;
        this.doctorsRegistry = doctorsRegistry;
    }

    public async Task<Doctor?> ResolveDoctorFromCredentials(string username, string password)
    {
        var validAccounts = await this.accountsRegistry.GetAccountsList().ToListAsync();

        var doctorAccount = validAccounts.Find(account => account.Username == username
        && account.Password == password);

        if (doctorAccount is null)
        {
            throw new KeyNotFoundException("Incorrect credentials");
        }

        var doctors = await this.doctorsRegistry.GetDoctorsList().ToListAsync();
        var doctor = doctors.Find(doctor => doctor.Id == doctorAccount.DoctorId);

        if (doctor is null)
        {
            throw new KeyNotFoundException("Account doesnt have a valid doctor");
        }

        return doctor;
    }
}
