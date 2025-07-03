using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Tournament.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;

namespace Tournament.Services;
public class GameService(IUnitOfWork unitOfWork, IMapper mapper) : IGameService
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IMapper mapper = mapper;

    public async Task<IEnumerable<GameDto>> GetAllGamesAsync()
    {
        var entities = await unitOfWork.GamesRepository.GetAllAsync();
        return mapper.Map<List<GameDto>>(entities);
    }

    public async Task<GameDto> GetGameByIdAsync(int id)
    {
        var game = await unitOfWork.GamesRepository.GetByIdAsync(id);
        return mapper.Map<GameDto>(game);
    }

    public async Task<GameDto> GetGameByTitleAsync(string title)
    {
        var game = await unitOfWork.GamesRepository.GetByTitleAsync(title);
        return mapper.Map<GameDto>(game);
    }

    public async Task<GameDto> UpdateGameAsync(int id, GameDto gameDto)
    {
        var game = await unitOfWork.GamesRepository.GetByIdAsync(id);
        mapper.Map(gameDto, game);
        await unitOfWork.PersistAllAsync();
        return gameDto;
    }

    public async Task<GameDto> PatchGameAsync(int id, JsonPatchDocument<GameDto> patchDoc)
    {
        var game = await GetGameByIdAsync(id); // needed?
        var gameDto = mapper.Map<GameDto>(patchDoc);
        patchDoc.ApplyTo(gameDto);
        mapper.Map(gameDto, game); // needed?
        await unitOfWork.PersistAllAsync();
        return gameDto;
    }

    public async Task<GameDto> CreateGameAsync(GameDto gameDto)
    {
        var game = mapper.Map<Game>(gameDto);
        unitOfWork.GamesRepository.Add(game);
        await unitOfWork.PersistAllAsync();
        return gameDto;
    }

    public async Task DeleteGameAsync(int id)
    {
        unitOfWork.GamesRepository.Delete(id);
        await unitOfWork.PersistAllAsync();
    }

    public async Task<bool> GameExistsAsync(int id)
    {
        return await unitOfWork.GamesRepository.AnyAsync(id);
    }
}
