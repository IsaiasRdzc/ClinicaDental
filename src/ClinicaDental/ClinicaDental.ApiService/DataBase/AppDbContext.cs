namespace ClinicaDental.ApiService.DataBase;

using ClinicaDental.ApiService.DataBase.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Supply> Supplies { get; init; }

    public DbSet<Appointment> Appointments { get; init; }

    public DbSet<Doctor> Doctors { get; init; }

    public DbSet<ClinicHours> ClinicHours { get; init; }

    public DbSet<ScheduleModification> ScheduleModifications { get; init; }
}
