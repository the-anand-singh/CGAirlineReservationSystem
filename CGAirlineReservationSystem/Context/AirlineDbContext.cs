using CGAirlineReservationSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGAirlineReservationSystem.DB_Context
{
    public class AirlineDbContext : DbContext
    {
        public AirlineDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Flight> Flights { get; set; }

    }
}
