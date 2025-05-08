using Microsoft.EntityFrameworkCore;
using MTGCardApi.Models;

namespace MTGCardApi.Data
{
    // the database context handles the connection to the database
    public class MTGDbContext : DbContext
    {
        // constructor receives options like connection string
        public MTGDbContext(DbContextOptions<MTGDbContext> options) : base(options) { }

        // this creates a table called "Cards" in the database
        public DbSet<Card> Cards { get; set; }
    }
}
