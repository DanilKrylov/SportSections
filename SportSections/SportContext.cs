using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportSections.Models;

namespace SportSections
{
    public class SportContext : IdentityDbContext<IdentityUser>
    {
        public SportContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Coaching> Coachings { get; set; }

        public DbSet<SectionRecord> SectionRecords { get; set; }

        public DbSet<SportSection> SportSections { get; set; }

        public DbSet<Sportsman> Sportsmans { get; set; }

        public DbSet<Sport> Sports { get; set; }

        public DbSet<Trainer> Trainers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=sports_db;Trusted_Connection=True;");
        }
    }
}
