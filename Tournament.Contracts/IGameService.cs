using Microsoft.AspNetCore.JsonPatch;
using Tournament.Core.Dto;
using Tournament.Core.Entities;

namespace Tournament.Contracts;
public interface IGameService
{
    Task<IEnumerable<GameDto>> GetAllGamesAsync(GamesParameters gamesParameters);
    Task<GameDto> GetGameByIdAsync(int id);
    Task<GameDto> GetGameByTitleAsync(string title);
    Task<GameDto> CreateGameAsync(GameDto gameDto);
    Task<GameDto> UpdateGameAsync(int id, GameDto gameDto);
    Task<GameDto> PatchGameAsync(int id, JsonPatchDocument<GameDto> patchDoc);
    Task DeleteGameAsync(int id);
    Task<bool> GameExistsAsync(int id);
}
