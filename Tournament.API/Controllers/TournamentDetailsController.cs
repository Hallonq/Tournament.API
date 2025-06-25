using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.Entities;

namespace Tournament.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TournamentDetailsController(TournamentAPIContext context) : ControllerBase
{
    // GET: api/TournamentDetails
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TournamentDetails>>> GetTournamentDetails()
    {
        return await context.TournamentDetails.Include(x => x.Games).ToListAsync();
    }

    // GET: api/TournamentDetails/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TournamentDetails>> GetTournamentDetails(int id)
    {
        var tournamentDetails = await context.TournamentDetails.FindAsync(id);

        if (tournamentDetails == null)
        {
            return NotFound();
        }

        return tournamentDetails;
    }

    // PUT: api/TournamentDetails/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTournamentDetails(int id, TournamentDetails tournamentDetails)
    {
        if (id != tournamentDetails.Id)
        {
            return BadRequest();
        }

        context.Entry(tournamentDetails).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TournamentDetailsExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/TournamentDetails
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TournamentDetails>> PostTournamentDetails(TournamentDetails tournamentDetails)
    {
        context.TournamentDetails.Add(tournamentDetails);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetTournamentDetails", new { id = tournamentDetails.Id }, tournamentDetails);
    }

    // DELETE: api/TournamentDetails/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTournamentDetails(int id)
    {
        var tournamentDetails = await context.TournamentDetails.FindAsync(id);
        if (tournamentDetails == null)
        {
            return NotFound();
        }

        context.TournamentDetails.Remove(tournamentDetails);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool TournamentDetailsExists(int id)
    {
        return context.TournamentDetails.Any(e => e.Id == id);
    }
}
