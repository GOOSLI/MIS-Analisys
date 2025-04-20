using Microsoft.EntityFrameworkCore;
using MisAnalisysWorker.Models;

namespace MisAnalisysWorker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ClinicalRecommendation> ClinicalRecommendations { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<AvailableService> AvailableServices { get; set; }
        public DbSet<PrescribedServiceParsed> PrescribedServicesParsed { get; set; }
        public DbSet<ScheduledService> ScheduledServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationship configuration
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.EmployeeTypeNavigation)
                .WithMany(t => t.Employees)
                .HasForeignKey(e => e.EmployeeType);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.DepartmentNavigation)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<PrescribedServiceParsed>()
                .HasOne(p => p.ServiceNavigation)
                .WithMany(s => s.PrescribedServicesParsed)
                .HasForeignKey(p => p.ServiceId);

            modelBuilder.Entity<PrescribedServiceParsed>()
                .HasOne(p => p.AppointmentNavigation)
                .WithMany(a => a.PrescribedServicesParsed)
                .HasForeignKey(p => p.AppointmentId);

            modelBuilder.Entity<ScheduledService>()
                .HasOne(s => s.ServiceNavigation)
                .WithMany(a => a.ScheduledServices)
                .HasForeignKey(s => s.ServiceId);

            modelBuilder.Entity<ScheduledService>()
                .HasOne(s => s.ScheduledByNavigation)
                .WithMany(e => e.ScheduledServices)
                .HasForeignKey(s => s.ScheduledById);

            modelBuilder.Entity<ScheduledService>()
                .HasOne(s => s.ScheduledForNavigation)
                .WithMany(p => p.ScheduledServices)
                .HasForeignKey(s => s.ScheduledForId);

            modelBuilder.Entity<ScheduledService>()
                .HasOne(s => s.AppointmentNavigation)
                .WithMany(a => a.ScheduledServices)
                .HasForeignKey(s => s.AppointmentId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.PatientNavigation)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.DoctorNavigation)
                .WithMany(e => e.DoctorAppointments)
                .HasForeignKey(a => a.DoctorId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.DiagnosisNavigation)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DiagnosisId);

            modelBuilder.Entity<ClinicalRecommendation>()
                .HasOne(c => c.DiagnosisNavigation)
                .WithMany(d => d.ClinicalRecommendations)
                .HasForeignKey(c => c.DiagnosisId);
        }
    }
} 