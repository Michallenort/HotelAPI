using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Entities
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {

        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(a => a.City).IsRequired().HasMaxLength(30);

            modelBuilder.Entity<Hotel>()
                .Property(h => h.Name).IsRequired().HasMaxLength(40);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.StartDate).IsRequired();

            modelBuilder.Entity<Reservation>()
                .Property(r => r.EndDate).IsRequired();

            modelBuilder.Entity<Room>()
                .Property(r => r.Number).IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Name);

            modelBuilder.Entity<User>()
                .Property(u => u.Email);

            
        }

    }
}
