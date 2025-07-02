using Microsoft.AspNetCore.JsonPatch;
using Tournament.Core.Dto;

namespace Tournament.Contracts;
public interface ITournamentService
{
    // Define methods that the TournamentService should implement
    // For example:
    Task<IEnumerable<TournamentDto>> GetAllTournamentsAsync(bool includeGames = false);
    Task<TournamentDto> GetTournamentByIdAsync(int id);
    Task<bool> CreateTournamentAsync(TournamentDto tournamentDto);
    Task<bool> UpdateTournamentAsync(int id, TournamentDto tournamentDto);
    Task<bool> DeleteTournamentAsync(int id);
    Task<bool> PatchTournamentAsync(int id, JsonPatchDocument<TournamentDto> patchDoc);
}
