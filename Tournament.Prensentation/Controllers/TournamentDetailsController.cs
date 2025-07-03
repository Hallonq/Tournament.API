using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Tournament.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Entities;

namespace Tournament.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TournamentDetailsController(ITournamentService tournamentService) : ControllerBase
{
    // GET: api/TournamentDetails
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournamentDetails([FromBody] bool includeGames = false)
    {
        var tournaments = await tournamentService.GetAllTournamentsAsync();
        return tournaments is null ? NotFound() : Ok(tournaments);
    }

    // GET: api/TournamentDetails/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TournamentDto>> GetTournamentDetails(int id)
    {
        var tournament = await tournamentService.GetTournamentByIdAsync(id);
        return tournament is null ? NotFound() : Ok(tournament);
    }

    // PUT: api/TournamentDetails/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTournamentDetails(int id, TournamentDto tournamentDto)
    {
        var tournament = await tournamentService.UpdateTournamentAsync(id, tournamentDto);
        return tournament is null ? NotFound() : Ok(tournament);
    }

    // PATCH: api/TournamentDetails
    [HttpPatch("{id}")]
    public async Task<ActionResult<TournamentDto>> PatchTournamentDetails(
        int id,
        JsonPatchDocument<TournamentDto> patchDoc)
    {
        if (patchDoc is null) { return BadRequest(); }
        var DTO = await tournamentService.PatchTournamentAsync(id, patchDoc);
        return DTO is null ? NotFound() : Ok(DTO);
    }

    // POST: api/TournamentDetails
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TournamentDetails>> PostTournamentDetails(TournamentDto tournamentDto)
    {
        var DTO = await tournamentService.CreateTournamentAsync(tournamentDto);
        return CreatedAtAction("GetTournamentDetails", DTO);
    }

    // DELETE: api/TournamentDetails/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTournamentDetails(int id)
    {
        await tournamentService.DeleteTournamentAsync(id);
        return NoContent();
    }
}
