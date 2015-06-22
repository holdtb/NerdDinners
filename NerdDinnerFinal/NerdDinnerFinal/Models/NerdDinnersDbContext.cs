using System.Data.Entity;

namespace NerdDinnerFinal.Models
{
    public class NerdDinnersDbContext : DbContext
    {
        public DbSet<Dinner> Dinners { get; set; }
        public DbSet<RSVP> RSVPs { get; set; }
    }
}