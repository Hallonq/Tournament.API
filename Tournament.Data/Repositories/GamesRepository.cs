using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Data.Data;

namespace Tournament.Data.Repositories;
public class GamesRepository(TournamentAPIContext context) : IGamesRepository
{
    public void Add(Game game) => context.Add(game);
    public async Task<bool> AnyAsync(int id) => await context.Game.Where(x => x.Id == id).AnyAsync();
    public async Task<IEnumerable<Game>> GetAllAsync(PaginationParameters paginationParameters)
    {
        paginationParameters.TotalItems = context.Game.Count();
        paginationParameters.TotalPages = (int)Math.Ceiling(paginationParameters.TotalItems / (double)paginationParameters.PageSize);

        return await Task.FromResult<IEnumerable<Game>>([.. context.Game.Skip((paginationParameters.CurrentPage - 1) * paginationParameters.PageSize).Take(paginationParameters.PageSize).AsNoTracking()]);
    }
    public async Task<Game> GetByTitleAsync(string? title) => await Task.FromResult<Game>(await context.Game.Where(x => x.Title == title).FirstOrDefaultAsync());
    public async Task<Game> GetByIdAsync(int id) => await Task.FromResult<Game>(await context.Game.FindAsync(id));
    public void Update(Game game) => context.Entry(game).State = EntityState.Modified;
    public void Delete(int id) => context.Remove(id);
}
