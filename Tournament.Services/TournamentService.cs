using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Tournament.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;

namespace Tournament.Services;
public class TournamentService(IUnitOfWork unitOfWork, IMapper mapper) : ITournamentService
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IMapper mapper = mapper;

    public async Task<IEnumerable<TournamentDto>> GetAllTournamentsAsync()
    {
        var entities = await unitOfWork.TournamentRepository.GetAllAsync();
        var DTOs = mapper.Map<List<TournamentDto>>(entities);
        return DTOs;
    }

    public async Task<TournamentDto> GetTournamentByIdAsync(int id)
    {
        var entity = await unitOfWork.TournamentRepository.GetAsync(id);
        var DTO = mapper.Map<TournamentDto>(entity);
        return DTO;
    }

    public async Task<TournamentDto> UpdateTournamentAsync(int id, TournamentDto tournamentDto)
    {
        var entity = await unitOfWork.TournamentRepository.GetAsync(id);
        mapper.Map(tournamentDto, entity);
        await unitOfWork.PersistAllAsync();
        return tournamentDto;
    }

    public async Task<TournamentDto> PatchTournamentAsync(int id, JsonPatchDocument<TournamentDto> patchDoc)
    {
        var entity = await unitOfWork.TournamentRepository.GetAsync(id);
        var tournamentDto = mapper.Map<TournamentDto>(entity);
        patchDoc.ApplyTo(tournamentDto);
        mapper.Map(tournamentDto, entity);
        await unitOfWork.PersistAllAsync();
        return tournamentDto;
    }

    public async Task<TournamentDto> CreateTournamentAsync(TournamentDto tournamentDto)
    {
        var entity = mapper.Map<TournamentDetails>(tournamentDto);
        try
        {
            unitOfWork.TournamentRepository.Add(entity);
            await unitOfWork.PersistAllAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return tournamentDto;
    }

    public async Task DeleteTournamentAsync(int id)
    {
        if (await unitOfWork.TournamentRepository.AnyAsync(id))
        {
            try
            {
                unitOfWork.TournamentRepository.Remove(id);
                await unitOfWork.PersistAllAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
