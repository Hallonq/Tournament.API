namespace Tournament.Contracts;
public interface IServiceManager
{
    public ITournamentService TournamentService { get; }
    public IGameService GameService { get; }
}
