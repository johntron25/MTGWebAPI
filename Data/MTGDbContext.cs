using Microsoft.EntityFrameworkCore;
using MTGCardApi.Models;

namespace MTGCardApi.Data
{
    public class MTGDbContext : DbContext
    {
        public MTGDbContext(DbContextOptions<MTGDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
    }
}
