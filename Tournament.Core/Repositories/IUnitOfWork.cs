namespace Tournament.Core.Repositories;
public interface IUnitOfWork
{
    ITournamentRepository TournamentRepository { get; }
    IGamesRepository GamesRepository { get; }
    Task PersistAllAsync();
}
