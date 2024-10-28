namespace ClinicaDental.ApiService.DataBase;

using ClinicaDental.ApiService.DataBase.Models;

using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Appointment> Appointments { get; init; }

    public DbSet<Supply> Supplies { get; set; }
    public DbSet<MedicalSupply> MedicalSupplies { get; set; }
    public DbSet<SurgicalSupply> SurgicalSupplies { get; set; }
    public DbSet<CleaningSupply> CleaningSupplies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Supply>()
            .ToTable("Supplies"); 

        modelBuilder.Entity<MedicalSupply>()
            .ToTable("MedicalSupplies"); 
        modelBuilder.Entity<SurgicalSupply>()
            .ToTable("SurgicalSupplies"); 
        modelBuilder.Entity<CleaningSupply>()
            .ToTable("CleaningSupplies"); 
    }
}
