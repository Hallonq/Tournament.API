using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories;
public class TournamentRepository(TournamentAPIContext context) : ITournamentRepository
{
    public void Add(TournamentDetails tournament) => context.Add(tournament);
    public async Task<bool> AnyAsync(int id) => await context.TournamentDetails.Where(x => x.Id == id).AnyAsync();
    public async Task<IEnumerable<TournamentDetails>> GetAllAsync(bool includeGames, PaginationParameters paginationParameters)
    {
        paginationParameters.TotalItems = context.TournamentDetails.Count();
        paginationParameters.TotalPages = (int)Math.Ceiling(paginationParameters.TotalItems / (double)paginationParameters.PageSize);
        if (includeGames)
        {
            // with games
            return await Task.FromResult<IEnumerable<TournamentDetails>>([.. context.TournamentDetails.Include(x => x.Games).Skip((paginationParameters.CurrentPage - 1) * paginationParameters.PageSize).Take(paginationParameters.PageSize).AsNoTracking()]);
        }

        // without games
        return await Task.FromResult<IEnumerable<TournamentDetails>>([.. context.TournamentDetails.Skip((paginationParameters.CurrentPage - 1) * paginationParameters.PageSize).Take(paginationParameters.PageSize).AsNoTracking()]);
    }
    public async Task<TournamentDetails> GetAsync(int id) => await Task.FromResult<TournamentDetails>(await context.TournamentDetails.FindAsync(id));
    public void Update(TournamentDetails tournament) => context.Entry(tournament).State = EntityState.Modified;
    public void Remove(int id) => context.Remove(id);
}
