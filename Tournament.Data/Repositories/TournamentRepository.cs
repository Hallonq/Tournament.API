using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories;
public class TournamentRepository(TournamentAPIContext context) : ITournamentRepository
{
    public void Add(TournamentDetails tournament) => context.Add(tournament);
    public async Task<bool> AnyAsync(int id) => await context.TournamentDetails.Where(x => x.Id == id).AnyAsync();
    public async Task<IEnumerable<TournamentDetails>> GetAllAsync(bool includeGames)
    {
        if (includeGames)
        {
            return await Task.FromResult<IEnumerable<TournamentDetails>>([.. context.TournamentDetails.Include(x => x.Games)/*.AsNoTracking()*/]);
        }

        return await Task.FromResult<IEnumerable<TournamentDetails>>([.. context.TournamentDetails.AsNoTracking()]);
    }
    public async Task<TournamentDetails> GetAsync(int id) => await Task.FromResult<TournamentDetails>(await context.TournamentDetails.FindAsync(id));
    public void Update(TournamentDetails tournament) => context.Entry(tournament).State = EntityState.Modified;
    public void Remove(int id) => context.Remove(id);
}
