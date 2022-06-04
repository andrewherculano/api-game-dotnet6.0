using GameApi.Data;
using GameApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Controllers
{
    [Route("v1/games")]
    public class GameController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Game>>> GetAll([FromServices] DataContext context)
        {
            try
            {
                var games = await context.Games
                    .Include(category => category.CategoryGame)
                    .ToListAsync();

                return Ok(games);
            }
            catch (Exception)
            {
                return BadRequest(new { messageError = "Não foi possível obter os jogos" });
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Game>> GetById(int id, [FromServices] DataContext context)
        {
            var game = await context.Games
                .Include(category => category.CategoryGame)
                .FirstOrDefaultAsync(game => game.Id == id);

            if (game == null)
                return NotFound(new { messageError = "Game não encontrado" });

            return Ok(game);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Game>> AddGame([FromBody] Game game, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            foreach (var item in context.Categories)
            {
                if (game.CategoryGameId == item.Id)
                {
                    context.Games.Add(game);
                    await context.SaveChangesAsync();
                    return Ok(game);
                }
            }

            return BadRequest(new { messageError = "Id da categoria inválido" });
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Game>> UpdateGame(int id, [FromBody] Game game, [FromServices] DataContext context)
        {
            if (game.Id != id)
                return NotFound(new { messageError = "Game não encontrado" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                foreach (var item in context.Categories)
                {
                    if (game.CategoryGameId == item.Id)
                    {
                        context.Entry<Game>(game).State = EntityState.Modified;
                        await context.SaveChangesAsync();
                        return Ok(game);
                    }
                }

                return BadRequest(new { messageError = "Id da categoria inválido" });
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { messageError = "Não foi possível atualizar a categoria" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Game>> DeleteGame(int id, [FromServices] DataContext context)
        {
            var game = await context.Games
                .FirstOrDefaultAsync(game => game.Id == id);

            if (game == null)
                return NotFound(new { messageError = "Game não encontrado" });

            try
            {
                context.Games.Remove(game);
                await context.SaveChangesAsync();
                return Ok(new { message = $"Game '{game.Title}' removido com sucesso" });
            }
            catch (Exception)
            {
                return BadRequest(new { messageError = "Não foi possível remover a categoria" });
            }
        }
    }
}