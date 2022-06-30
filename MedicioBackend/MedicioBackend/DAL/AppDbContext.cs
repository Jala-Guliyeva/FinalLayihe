using MedicioBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicioBackend.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        public DbSet<Slider>Sliders { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Question>Questions { get; set; }
        public DbSet<Bio>Bios { get; set; }
    }
}
