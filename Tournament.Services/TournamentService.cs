using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Tournament.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Repositories;

namespace Tournament.Services;
public class TournamentService(IUnitOfWork unitOfWork, IMapper mapper) : ITournamentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public Task<bool> CreateTournamentAsync(TournamentDto tournamentDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTournamentAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TournamentDto>> GetAllTournamentsAsync(bool includeGames = false)
    {
        throw new NotImplementedException();
    }

    public Task<TournamentDto> GetTournamentByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PatchTournamentAsync(int id, JsonPatchDocument<TournamentDto> patchDoc)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateTournamentAsync(int id, TournamentDto tournamentDto)
    {
        throw new NotImplementedException();
    }
}
