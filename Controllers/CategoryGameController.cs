using GameApi.Data;
using GameApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Controllers
{
    [ApiController]
    [Route("v1/categories")]
    public class CategoryGameController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<CategoryGame>>> GetAll([FromServices] DataContext context)
        {
            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(categories);
            }
            catch (Exception)
            {
                return BadRequest(new { messageError = "Não foi possível obter as categorias" });
            }

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<CategoryGame>> GetById(int id, [FromServices] DataContext context)
        {
            var categoryGame = await context.Categories.FirstOrDefaultAsync(category => category.Id == id);

            if (categoryGame == null)
                return NotFound(new { messageError = "Categoria não encontrada" });

            return Ok(categoryGame);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<CategoryGame>> AddCategory([FromBody] CategoryGame categoryGame, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Categories.Add(categoryGame);
                await context.SaveChangesAsync();
                return Ok(categoryGame);
            }
            catch (Exception)
            {
                return BadRequest(new { messageError = "Não foi possível cadastrar a categoria" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<CategoryGame>> UpdateCategory(int id, [FromBody] CategoryGame categoryGame, [FromServices] DataContext context)
        {
            if (id != categoryGame.Id)
                return NotFound(new { messageError = "Categoria não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<CategoryGame>(categoryGame).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(categoryGame);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { messageError = "Erro ao atualizar categoria" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<CategoryGame>> DeleteCategory(int id, [FromServices] DataContext context)
        {
            var category = context.Categories.FirstOrDefault(category => category.Id == id);

            if (category == null)
                return NotFound(new { messageError = "Categoria não encontrada" });

            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok(category);
            }
            catch (Exception)
            {
                return BadRequest(new { messageError = "Não foi possível remover a categoria" });
            }
        }
    }
}