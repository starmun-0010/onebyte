using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OneByte.DomainModels;

namespace OneByte.Data
{
    public class OneByteDbContext : IdentityDbContext 
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }

        public OneByteDbContext(DbContextOptions<OneByteDbContext> options) : base(options)
        {
        }
    }
}