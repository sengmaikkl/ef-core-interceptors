using EF_Core_Interceptors.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Interceptors.DataAccess
{
    public class Employee_DBContext : DbContext
    {
        public Employee_DBContext(DbContextOptions<Employee_DBContext> options)
       : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // ALERT - DEMO CODE
            // You may want to inject the interceptors into the context 
            optionsBuilder.AddInterceptors(new DemoDbCommandInterceptor());
        }

        //public DbSet<Student> Students { get; set; }
        public virtual DbSet<Student> Students { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");
               
            });
        }
    }
}
