using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Tournament.Contracts;
using Tournament.Core.Dto;
using Tournament.Core.Entities;

namespace Tournament.Prensentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GamesController(IServiceManager serviceManager) : ControllerBase
{
    // GET: api/Games
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGames([FromQuery] PaginationParameters paginationParameters)
    {
        var games = await serviceManager.GameService.GetAllGamesAsync(paginationParameters);

        Response.Headers.Add("X-Total-Count", paginationParameters.TotalItems.ToString());
        Response.Headers.Add("X-Page-Size", paginationParameters.PageSize.ToString());
        Response.Headers.Add("X-Current-Page", paginationParameters.CurrentPage.ToString());
        Response.Headers.Add("X-Total-Pages", paginationParameters.TotalPages.ToString());

        return games is null ? NotFound() : Ok(games);
    }

    // GET: api/Games/5
    [HttpGet("{title}")]
    public async Task<ActionResult<GameDto>> GetGame(string title)
    {
        var game = await serviceManager.GameService.GetGameByTitleAsync(title);
        return game is null ? NotFound() : Ok(game);
    }

    // PUT: api/Games/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGame(int id, GameDto gameDto)
    {
        var game = await serviceManager.GameService.UpdateGameAsync(id, gameDto);
        return game is null ? NotFound() : Ok(game);
    }

    // PATCH: api/Games
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchGame(
        int id,
        [FromBody] JsonPatchDocument<GameDto> patchDoc)
    {
        if (patchDoc is null) { return BadRequest(); }
        var game = await serviceManager.GameService.PatchGameAsync(id, patchDoc);
        return Ok(game);
    }

    // POST: api/Games
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<GameDto>> PostGame(GameDto gameDto)
    {
        var game = await serviceManager.GameService.CreateGameAsync(gameDto);
        return CreatedAtAction("GetGame", new { title = game.Title }, game);
    }

    // DELETE: api/Games/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(int id)
    {
        var game = await serviceManager.GameService.GameExistsAsync(id);
        if (!game)
            return NotFound();

        try
        {
            await serviceManager.GameService.DeleteGameAsync(id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return NoContent();
    }
}
