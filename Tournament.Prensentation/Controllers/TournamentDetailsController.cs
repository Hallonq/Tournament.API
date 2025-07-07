using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Tournament.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Entities;

namespace Tournament.Prensentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TournamentDetailsController(IServiceManager serviceManager) : ControllerBase
{
    // GET: api/TournamentDetails
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournamentDetails([FromBody] bool includeGames = false)
    {
        var tournaments = await serviceManager.TournamentService.GetAllTournamentsAsync();
        return tournaments is null ? NotFound() : Ok(tournaments);
    }

    // GET: api/TournamentDetails/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TournamentDto>> GetTournamentDetails(int id)
    {
        var tournament = await serviceManager.TournamentService.GetTournamentByIdAsync(id);
        return tournament is null ? NotFound() : Ok(tournament);
    }

    // PUT: api/TournamentDetails/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTournamentDetails(int id, TournamentDto tournamentDto)
    {
        var tournament = await serviceManager.TournamentService.UpdateTournamentAsync(id, tournamentDto);
        return tournament is null ? NotFound() : Ok(tournament);
    }

    // PATCH: api/TournamentDetails
    [HttpPatch("{id}")]
    public async Task<ActionResult<TournamentDto>> PatchTournamentDetails(
        int id,
        JsonPatchDocument<TournamentDto> patchDoc)
    {
        if (patchDoc is null) { return BadRequest(); }
        var DTO = await serviceManager.TournamentService.PatchTournamentAsync(id, patchDoc);
        return DTO is null ? NotFound() : Ok(DTO);
    }

    // POST: api/TournamentDetails
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TournamentDetails>> PostTournamentDetails(TournamentDto tournamentDto)
    {
        var DTO = await serviceManager.TournamentService.CreateTournamentAsync(tournamentDto);
        return CreatedAtAction("GetTournamentDetails", DTO);
    }

    // DELETE: api/TournamentDetails/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTournamentDetails(int id)
    {
        await serviceManager.TournamentService.DeleteTournamentAsync(id);
        return NoContent();
    }
}
