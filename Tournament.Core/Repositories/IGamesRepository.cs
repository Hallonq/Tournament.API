using Tournament.Core.Entities;

namespace Tournament.Core.Repositories;
public interface IGamesRepository
{
    Task<IEnumerable<Game>> GetAllAsync(PaginationParameters paginationParameters);
    Task<Game> GetByTitleAsync(string? title);
    Task<Game> GetByIdAsync(int id);
    Task<bool> AnyAsync(int id);
    void Add(Game game);
    void Update(Game game);
    void Delete(int id);
}
