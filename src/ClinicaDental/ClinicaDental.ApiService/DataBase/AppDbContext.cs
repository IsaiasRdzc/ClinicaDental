namespace ClinicaDental.ApiService.DataBase;

using ClinicaDental.ApiService.DataBase.Models.Appointments;
using ClinicaDental.ApiService.DataBase.Models.HumanResources;
using ClinicaDental.ApiService.DataBase.Models.Inventory;
using ClinicaDental.ApiService.DataBase.Models.Login;
using ClinicaDental.ApiService.DataBase.Models.MedicalRecords;
using ClinicaDental.ApiService.DataBase.Models.Purchases;

using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public required DbSet<Supply> Supplies { get; init; }

    public required DbSet<MedicalSupply> MedicalSupplies { get; init; }

    public required DbSet<SurgicalSupply> SurgicalSupplies { get; init; }

    public required DbSet<CleaningSupply> CleaningSupplies { get; init; }

    public required DbSet<Account> AccountsTable { get; init; }

    public required DbSet<Appointment> AppointmentsTable { get; init; }

    public required DbSet<Doctor> DoctorsTable { get; init; }

    public required DbSet<ClinicDayBussinesHours> ClinicDayBussinesHoursTable { get; init; }

    public required DbSet<DoctorDaySchedule> DoctorDaySchedulesTable { get; init; }

    public required DbSet<ScheduleModification> ScheduleModificationsTable { get; init; }

    public required DbSet<PaymentDetail> PaymentDetails { get; init; }

    public required DbSet<MedicalRecord> MedicalRecords { get; init; }

    public required DbSet<Patient> Patients { get; init; }

    public DbSet<Purchase> Purchases { get; set; } = null!;

    public DbSet<PurchaseDetail> PurchaseDetails { get; set; } = null!;

    public DbSet<Supplier> Suppliers { get; set; } = null!;

    public DbSet<PurchaseType> PurchaseTypes { get; set; } = null!;

    public DbSet<Material> Materials { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed ClinicHours
        modelBuilder.Entity<ClinicDayBussinesHours>().HasData(
            new ClinicDayBussinesHours(1) { DayOfWeek = DayOfWeek.Sunday, OpeningTime = new TimeOnly(0, 0), ClosingTime = new TimeOnly(0, 0), IsClosed = true },
            new ClinicDayBussinesHours(2) { DayOfWeek = DayOfWeek.Monday, OpeningTime = new TimeOnly(9, 0), ClosingTime = new TimeOnly(18, 0) },
            new ClinicDayBussinesHours(3) { DayOfWeek = DayOfWeek.Tuesday, OpeningTime = new TimeOnly(9, 0), ClosingTime = new TimeOnly(18, 0) },
            new ClinicDayBussinesHours(4) { DayOfWeek = DayOfWeek.Wednesday, OpeningTime = new TimeOnly(9, 0), ClosingTime = new TimeOnly(18, 0) },
            new ClinicDayBussinesHours(5) { DayOfWeek = DayOfWeek.Thursday, OpeningTime = new TimeOnly(9, 0), ClosingTime = new TimeOnly(18, 0) },
            new ClinicDayBussinesHours(6) { DayOfWeek = DayOfWeek.Friday, OpeningTime = new TimeOnly(9, 0), ClosingTime = new TimeOnly(18, 0) },
            new ClinicDayBussinesHours(7) { DayOfWeek = DayOfWeek.Saturday, OpeningTime = new TimeOnly(9, 0), ClosingTime = new TimeOnly(14, 0) });

        // Seed Doctor
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor(1) { Name = "Angela Maria Rubio" });

        // Seed DoctorSchedule
        modelBuilder.Entity<DoctorDaySchedule>().HasData(
            new DoctorDaySchedule(1) { DoctorId = 1, DayOfWeek = DayOfWeek.Sunday, StartTime = new TimeOnly(0, 0), EndTime = new TimeOnly(0, 0) },
            new DoctorDaySchedule(2) { DoctorId = 1, DayOfWeek = DayOfWeek.Monday, StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(15, 0) },
            new DoctorDaySchedule(3) { DoctorId = 1, DayOfWeek = DayOfWeek.Tuesday, StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(15, 0) },
            new DoctorDaySchedule(4) { DoctorId = 1, DayOfWeek = DayOfWeek.Wednesday, StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(16, 0) },
            new DoctorDaySchedule(5) { DoctorId = 1, DayOfWeek = DayOfWeek.Thursday, StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(16, 0) },
            new DoctorDaySchedule(6) { DoctorId = 1, DayOfWeek = DayOfWeek.Friday, StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(16, 0) },
            new DoctorDaySchedule(7) { DoctorId = 1, DayOfWeek = DayOfWeek.Saturday, StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(14, 0) });

        // Seed Accounts
        modelBuilder.Entity<Account>().HasData(
            new Account(1) { Username = "Admin", Password = "123", DoctorId = 1 });

        modelBuilder.Entity<Supply>().ToTable("Supplies");
        modelBuilder.Entity<MedicalSupply>().ToTable("MedicalSupplies");
        modelBuilder.Entity<SurgicalSupply>().ToTable("SurgicalSupplies");
        modelBuilder.Entity<CleaningSupply>().ToTable("CleaningSupplies");

        modelBuilder.Entity<MedicalRecord>()
            .HasMany(r => r.Diagnosis)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MedicalRecord>()
            .HasMany(r => r.MedicalProcedures)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MedicalRecord>()
            .HasMany(r => r.Teeths)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Illness>()
           .HasMany(r => r.Treatments) // medicine
           .WithOne()
           .OnDelete(DeleteBehavior.Cascade);

        // Configuración para la entidad Purchase
            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedDate).IsRequired();
                entity.HasOne(e => e.Supplier)
                    .WithMany() // Ya no hay relación con Purchases
                    .HasForeignKey(e => e.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        // Configuración para la entidad PurchaseDetail
        modelBuilder.Entity<PurchaseDetail>(entity =>
        {
            entity.HasKey(e => e.Id); // Primary Key
            entity.HasOne<Material>(e => e.Material)
                .WithMany()
                .HasForeignKey(e => e.MaterialId)
                .OnDelete(DeleteBehavior.Restrict); // Relación con Material
        });

        // Configuración para la entidad Supplier
        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id); // Primary Key
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100); // Campo requerido
            entity.Property(e => e.PhoneNumber).HasMaxLength(15); // Opcional
        });

        // Configuración para la entidad Material
        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id); // Primary Key
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100); // Campo requerido
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)"); // Definir precisión decimal
        });

        // Configuración para la entidad PurchaseType
        modelBuilder.Entity<PurchaseType>(entity =>
        {
            entity.HasKey(e => e.Id); // Primary Key
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50); // Campo requerido
        });
    }
}
