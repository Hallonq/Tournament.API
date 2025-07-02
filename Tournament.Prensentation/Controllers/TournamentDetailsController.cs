using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Tournament.Core.Dto;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;

namespace Tournament.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TournamentDetailsController(IMapper mapper, IUnitOfWork unitOfWork) : ControllerBase
{
    // GET: api/TournamentDetails
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournamentDetails([FromBody] bool includeGames = false)
    {
        var entities = await unitOfWork.TournamentRepository.GetAllAsync(includeGames);
        if (entities is null) { return NotFound(); }
        var DTOs = mapper.Map<List<TournamentDto>>(entities);
        return Ok(DTOs);
    }

    // GET: api/TournamentDetails/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TournamentDto>> GetTournamentDetails(int id)
    {
        var entity = await unitOfWork.TournamentRepository.GetAsync(id);
        if (entity is null) { return NotFound(); }
        var tournamentDTO = mapper.Map<TournamentDto>(entity);
        return Ok(tournamentDTO);
    }

    // PUT: api/TournamentDetails/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTournamentDetails(int id, TournamentDto tournamentDto)
    {
        var exist = await unitOfWork.TournamentRepository.AnyAsync(id);
        if (!exist) { return NotFound(); }
        mapper.Map<TournamentDto>(tournamentDto);
        await unitOfWork.PersistAllAsync();
        return NoContent();
    }

    // PATCH: api/TournamentDetails
    [HttpPatch("{id}")]
    public async Task<ActionResult<TournamentDto>> PatchTournamentDetails(
        int id,
        JsonPatchDocument<TournamentDto> patchDoc)
    {
        if (patchDoc is null) { return BadRequest(); }
        var tournamentDetails = await unitOfWork.TournamentRepository.GetAsync(id);
        if (tournamentDetails is null) { return NotFound(); }

        var tournamentDto = mapper.Map<TournamentDto>(tournamentDetails);
        patchDoc.ApplyTo(tournamentDto, ModelState);
        if (!TryValidateModel(tournamentDto))
        {
            return BadRequest(ModelState);
        }

        mapper.Map(tournamentDto, tournamentDetails);
        await unitOfWork.PersistAllAsync();
        return Ok(tournamentDetails);
    }

    // POST: api/TournamentDetails
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TournamentDetails>> PostTournamentDetails(TournamentDto tournamentDto)
    {
        var tournamentDetails = mapper.Map<TournamentDetails>(tournamentDto);
        try
        {
            unitOfWork.TournamentRepository.Add(tournamentDetails);
            await unitOfWork.PersistAllAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return CreatedAtAction("GetTournamentDetails", new { id = tournamentDetails.Id }, tournamentDetails);
    }

    // DELETE: api/TournamentDetails/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTournamentDetails(int id)
    {
        var tournamentDetails = await unitOfWork.TournamentRepository.AnyAsync(id);
        if (!tournamentDetails)
            return NotFound();

        unitOfWork.TournamentRepository.Remove(id);
        await unitOfWork.PersistAllAsync();
        return NoContent();
    }
}
