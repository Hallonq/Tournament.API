using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;

namespace Tournament.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TournamentDetailsController(IUnitOfWork unitOfWork) : ControllerBase
{
    // GET: api/TournamentDetails
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TournamentDetails>>> GetTournamentDetails()
    {
        return Ok(await unitOfWork.TournamentRepository.GetAllAsync());
    }

    // GET: api/TournamentDetails/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TournamentDetails>> GetTournamentDetails(int id)
    {
        var tournamentDetails = await unitOfWork.TournamentRepository.GetAsync(id);
        return tournamentDetails is null ? NotFound() : Ok(tournamentDetails);
    }

    // PUT: api/TournamentDetails/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTournamentDetails(int id, TournamentDetails tournamentDetails)
    {
        if (id != tournamentDetails.Id)
            return BadRequest();

        try
        {
            unitOfWork.TournamentRepository.Update(tournamentDetails);
            await unitOfWork.PersistAllAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await TournamentDetailsExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // POST: api/TournamentDetails
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TournamentDetails>> PostTournamentDetails(TournamentDetails tournamentDetails)
    {
        unitOfWork.TournamentRepository.Add(tournamentDetails);
        await unitOfWork.PersistAllAsync();

        return CreatedAtAction("GetTournamentDetails", new { id = tournamentDetails.Id }, tournamentDetails);
    }

    // DELETE: api/TournamentDetails/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTournamentDetails(int id)
    {
        var tournamentDetails = await unitOfWork.TournamentRepository.GetAsync(id);
        if (tournamentDetails is null)
            return NotFound();

        unitOfWork.TournamentRepository.Remove(tournamentDetails);
        await unitOfWork.PersistAllAsync();

        return NoContent();
    }

    private async Task<bool> TournamentDetailsExists(int id)
    {
        return await unitOfWork.TournamentRepository.AnyAsync(id);
    }
}
