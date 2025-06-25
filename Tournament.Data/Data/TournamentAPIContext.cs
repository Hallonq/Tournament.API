using Microsoft.EntityFrameworkCore;

namespace Tournament.Data.Data
{
    public class TournamentAPIContext : DbContext
    {
        public TournamentAPIContext(DbContextOptions<TournamentAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Core.Entities.TournamentDetails> TournamentDetails { get; set; } = default!;
        public DbSet<Core.Entities.Game> Game { get; set; } = default!;
    }
}
