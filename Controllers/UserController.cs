using GameApi.Data;
using GameApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Controllers
{
    [ApiController]
    [Route("v1/users")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<User>>> GetAll([FromServices] DataContext context)
        {
            var users = await context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> GetById(int id, [FromServices] DataContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
                return NotFound(new { messageError = "Usuário não encontrado!" });

            return Ok(user);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<User>> AddUser([FromBody] User user, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest(new { messageError = "Não foi possível cadastrar o usuário" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user, [FromServices] DataContext context)
        {
            if (id != user.Id)
                return NotFound(new { messageError = "Usuário não encontrado" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<User>(user).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { messageError = "Erro ao atualizar usuário" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> DeleteUser(int id, [FromServices] DataContext context)
        {
            var user = context.Users.FirstOrDefault(user => user.Id == id);

            if (user == null)
                return NotFound(new { messageError = "Usuário não encontrado" });

            try
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest(new { messageError = "Não foi possível remover o usuário" });
            }
        }
    }
}