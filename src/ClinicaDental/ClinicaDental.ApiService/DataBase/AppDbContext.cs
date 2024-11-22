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

        // Seed de MedicalSupply
        modelBuilder.Entity<MedicalSupply>().HasData(
            new MedicalSupply(1) { Name = "Ibuprofeno", Stock = 50, ExpirationDate = new DateOnly(2025, 12, 31), MedicationType = "Analgésico", LotNumber = "L12345" },
            new MedicalSupply(2) { Name = "Paracetamol", Stock = 75, ExpirationDate = new DateOnly(2024, 8, 31), MedicationType = "Antipirético", LotNumber = "L67890" },
            new MedicalSupply(3) { Name = "Amoxicilina", Stock = 40, ExpirationDate = new DateOnly(2026, 3, 15), MedicationType = "Antibiótico", LotNumber = "L54321" },
            new MedicalSupply(4) { Name = "Insulina", Stock = 25, ExpirationDate = new DateOnly(2025, 1, 20), MedicationType = "Hormona", LotNumber = "L78901" },
            new MedicalSupply(5) { Name = "Cloruro de Sodio 0.9%", Stock = 100, ExpirationDate = new DateOnly(2024, 5, 10), MedicationType = "Suero", LotNumber = "L23456" });

        // Seed de SurgicalSupply
        modelBuilder.Entity<SurgicalSupply>().HasData(
            new SurgicalSupply(6) { Name = "Bisturí", Stock = 20, SurgicalType = "Corte", SterilizationMethod = "Autoclave", SterilizationDate = new DateOnly(2023, 11, 1) },
            new SurgicalSupply(7) { Name = "Pinzas", Stock = 15, SurgicalType = "Sujeción", SterilizationMethod = "Autoclave", SterilizationDate = new DateOnly(2023, 11, 1) },
            new SurgicalSupply(8) { Name = "Tijeras Quirúrgicas", Stock = 10, SurgicalType = "Corte", SterilizationMethod = "Esterilización química", SterilizationDate = new DateOnly(2023, 11, 2) },
            new SurgicalSupply(9) { Name = "Suturas", Stock = 50, SurgicalType = "Cierre de heridas", SterilizationMethod = "Radiación gamma", SterilizationDate = new DateOnly(2023, 10, 15) },
            new SurgicalSupply(10) { Name = "Guantes Quirúrgicos", Stock = 100, SurgicalType = "Protección", SterilizationMethod = "Radiación gamma", SterilizationDate = new DateOnly(2023, 10, 20) });

        // Seed de CleaningSupply
        modelBuilder.Entity<CleaningSupply>().HasData(
            new CleaningSupply(11) { Name = "Desinfectante de Superficies", Stock = 30, CleaningType = "Desinfección", CleaningMethod = "Spray", CleaningDate = new DateOnly(2023, 11, 5) },
            new CleaningSupply(12) { Name = "Jabón Líquido", Stock = 50, CleaningType = "Limpieza", CleaningMethod = "Lavado manual", CleaningDate = new DateOnly(2023, 11, 1) },
            new CleaningSupply(13) { Name = "Alcohol 70%", Stock = 60, CleaningType = "Desinfección", CleaningMethod = "Aplicación directa", CleaningDate = new DateOnly(2023, 11, 4) },
            new CleaningSupply(14) { Name = "Cloro", Stock = 25, CleaningType = "Limpieza profunda", CleaningMethod = "Mezcla con agua", CleaningDate = new DateOnly(2023, 11, 3) },
            new CleaningSupply(15) { Name = "Toallas Desinfectantes", Stock = 40, CleaningType = "Limpieza rápida", CleaningMethod = "Uso directo", CleaningDate = new DateOnly(2023, 11, 2) });
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
