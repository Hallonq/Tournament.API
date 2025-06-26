using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories;
public class UnitOfWork(TournamentAPIContext context,
    ITournamentRepository tournamentRepository,
    IGamesRepository gamesRepository) : IUnitOfWork
{
    public ITournamentRepository TournamentRepository => tournamentRepository;

    public IGamesRepository GamesRepository => gamesRepository;

    public Task PersistAllAsync()
    {
        return context.SaveChangesAsync();
    }
}
