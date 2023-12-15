using EmployeesService.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Globalization;

namespace EmployeesService.Api
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Department> Departments { get; set; }
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
       : base(options)
        {
            //Database.EnsureCreated();   // создаем базу данных при первом обращении

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Определение отношения "один ко многим"
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.DepartmentId);

            // Определение отношения "один к одному"
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Passport)
                .WithOne(p => p.Employee)
                .HasForeignKey<Employee>(e => e.PassportId);
        }

    }
}
