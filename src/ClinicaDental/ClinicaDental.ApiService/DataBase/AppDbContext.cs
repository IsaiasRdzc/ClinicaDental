namespace ClinicaDental.ApiService.DataBase;

using ClinicaDental.ApiService.DataBase.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public required DbSet<Supply> Supplies { get; init; }

    public required DbSet<Appointment> Appointments { get; init; }

    public required DbSet<Doctor> Doctors { get; init; }

    public required DbSet<ClinicDayBussinesHours> ClinicDayBussinesHours { get; init; }

    public required DbSet<DoctorDaySchedule> DoctorDaySchedules { get; init; }

    public required DbSet<ScheduleModification> ScheduleModifications { get; init; }

    public DbSet<PaymentDetail> PaymentDetails { get; init; }

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
    }
}
