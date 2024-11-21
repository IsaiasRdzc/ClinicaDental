namespace ClinicaDental.ApiService.Login;

using System.Data;

using ClinicaDental.ApiService.DataBase.Models.Doctors;
using ClinicaDental.ApiService.DataBase.Models.Login;
using ClinicaDental.ApiService.DataBase.Registries.Doctors;
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

    public async Task<Doctor?> GetDoctorByLogin(string username, string password)
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
        return doctor;
    }
}
