using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Employment
{
    public class EmployeeContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<SalGrade> SalGrades { get; set; } 

        public EmployeeContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-6POC7D5\\SQLEXPRESS;Database=EmployeeDB;Trusted_Connection=True;");
            Console.WriteLine("Ok!!!!!!!!!!!!!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Department table
            modelBuilder.Entity<Department>().Property(d => d.DepartmentId)
                .ValueGeneratedNever();
            modelBuilder.Entity<Department>().Initialize();

            //Employee table
            modelBuilder.Entity<Employee>().Property(e => e.EmployeeId)
                .ValueGeneratedNever();
            modelBuilder.Entity<Employee>().Property(e => e.Name)
                .IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Salary)
               .HasDefaultValue(1250)
               .IsRequired();
            modelBuilder.Entity<Employee>().Property(e=>e.Job).IsRequired();
            modelBuilder.Entity<Employee>().Property(e=>e.HireDate).IsRequired();

            modelBuilder.Entity<Employee>().Initialize();

            //SalGrade table
            modelBuilder.Entity<SalGrade>().Initialize();

            base.OnModelCreating(modelBuilder);
                                    
        }
    }
}
