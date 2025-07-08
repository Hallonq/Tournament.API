using Microsoft.AspNetCore.JsonPatch;
using Tournament.Core.Dto;

namespace Tournament.Contracts;
public interface ITournamentService
{
    // Define methods that the TournamentService should implement
    // For example:
    Task<IEnumerable<TournamentDto>> GetAllTournamentsAsync();
    Task<TournamentDto> GetTournamentByIdAsync(int id);
    Task<TournamentDto> UpdateTournamentAsync(int id, TournamentDto tournamentDto);
    Task<TournamentDto> PatchTournamentAsync(int id, JsonPatchDocument<TournamentDto> patchDoc);
    Task<TournamentDto> CreateTournamentAsync(TournamentDto tournamentDto);
    Task DeleteTournamentAsync(int id);
}
