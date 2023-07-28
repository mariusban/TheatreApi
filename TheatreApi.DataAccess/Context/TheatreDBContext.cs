using Microsoft.EntityFrameworkCore;
using TheatreApi.DataAccess.Models;

namespace TheatreApi.DataAccess.Context
{
    public class TheatreDBContext : DbContext
    {
        public TheatreDBContext(DbContextOptions<TheatreDBContext> options)
            : base(options)
        {
        }

        public DbSet<Theatre> Theatre { get; set; }
        public DbSet<Play> Play { get; set; }
        public DbSet<Actor> Actor { get; set; }
    }
}
