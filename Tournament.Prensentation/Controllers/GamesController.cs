using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Tournament.Contracts;
using Tournament.Core.Dto;

namespace Tournament.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController(IGameService gameService) : ControllerBase
{
    // GET: api/Games
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
    {
        var games = await gameService.GetAllGamesAsync();
        return games is null ? NotFound() : Ok(games);
    }

    // GET: api/Games/5
    [HttpGet("{title}")]
    public async Task<ActionResult<GameDto>> GetGame(string title)
    {
        var game = await gameService.GetGameByTitleAsync(title);
        return game is null ? NotFound() : Ok(game);
    }

    // PUT: api/Games/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGame(int id, GameDto gameDto)
    {
        var game = await gameService.UpdateGameAsync(id, gameDto);
        return game is null ? NotFound() : Ok(game);
    }

    // PATCH: api/Games
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchGame(
        int id,
        [FromBody] JsonPatchDocument<GameDto> patchDoc)
    {
        if (patchDoc is null) { return BadRequest(); }
        var game = await gameService.PatchGameAsync(id, patchDoc);
        return Ok(game);
    }

    // POST: api/Games
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<GameDto>> PostGame(GameDto gameDto)
    {
        var game = await gameService.CreateGameAsync(gameDto);
        return CreatedAtAction("GetGame", new { game });
    }

    // DELETE: api/Games/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(int id)
    {
        var game = await gameService.GameExistsAsync(id);
        if (!game)
            return NotFound();

        try
        {
            await gameService.DeleteGameAsync(id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return NoContent();
    }
}
