using Tournament.Contracts;

namespace Tournament.Services;
public class ServiceManager
    (ITournamentService tournamentService,
    IGameService gameService) : IServiceManager
{
    public ITournamentService TournamentService => tournamentService;
    public IGameService GameService => gameService;
}
