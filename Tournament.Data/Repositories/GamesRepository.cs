using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories;
public class GamesRepository(TournamentAPIContext context) : IGamesRepository
{
    public void Add(Game game)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(int id)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Game>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Game> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Game game)
    {
        throw new NotImplementedException();
    }
}
