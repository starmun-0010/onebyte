using Microsoft.EntityFrameworkCore;
using OneByte.Models;

namespace OneByte.Data
{
    public class OneByteDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=172.17.0.2;Username=postgres;Password=admin;Database=onebyte");
        }
    }
}