using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Tournament.Core.Dto;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;

namespace Tournament.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController(IMapper mapper, IUnitOfWork unitOfWork) : ControllerBase
{
    // GET: api/Games
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGame()
    {
        var entities = await unitOfWork.GamesRepository.GetAllAsync();
        if (entities is null) { return NotFound(); }
        var DTOs = mapper.Map<List<GameDto>>(entities);
        return Ok(DTOs);
    }

    // GET: api/Games/5
    [HttpGet("{title}")]
    public async Task<ActionResult<GameDto>> GetGame(string? title)
    {
        var game = await unitOfWork.GamesRepository.GetByTitleAsync(title);
        if (game == null) { return NotFound(); }
        var gameDto = mapper.Map<GameDto>(game);
        return Ok(gameDto);
    }

    // PUT: api/Games/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGame(int id, GameDto gameDto)
    {
        var exist = await unitOfWork.GamesRepository.AnyAsync(id);
        if (!exist) { return NotFound(); }
        mapper.Map<GameDto>(gameDto);
        await unitOfWork.PersistAllAsync();
        return NoContent();
    }

    // PATCH: api/Games
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchGame(
        int id, 
        [FromBody] JsonPatchDocument<GameDto> patchDoc)
    {
        if (patchDoc is null) { return BadRequest(); }
        var game = await unitOfWork.GamesRepository.GetByIdAsync(id);
        if (game is null) { return NotFound(); }

        var gameDto = mapper.Map<GameDto>(game);
        patchDoc.ApplyTo(gameDto, ModelState);
        if (!TryValidateModel(gameDto))
        {
            return BadRequest(ModelState);
        }

        mapper.Map(gameDto, game);
        await unitOfWork.PersistAllAsync();
        return Ok(game);
    }

    // POST: api/Games
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<GameDto>> PostGame(GameDto gameDto)
    {
        var game = mapper.Map<Game>(gameDto);
        try
        {
            unitOfWork.GamesRepository.Add(game);
            await unitOfWork.PersistAllAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return CreatedAtAction("GetGame", new { id = game.Id }, game);
    }

    // DELETE: api/Games/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(int id)
    {
        var game = await unitOfWork.GamesRepository.AnyAsync(id);
        if (!game)
            return NotFound();

        unitOfWork.GamesRepository.Delete(id);
        await unitOfWork.PersistAllAsync();
        return NoContent();
    }
}
