using GameApi.Data;
using GameApi.Models;
using GameApi.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace GameApi.Controllers
{
    [Route("v1/data")]
    public class DataController
    {
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<dynamic>> EnterData([FromServices] DataContext context)
        {
            var category = new CategoryGame { Id = 1, Title = "MOBA", IdPlatform = GamePlatform.Xbox };
            var category2 = new CategoryGame { Id = 2, Title = "RPG", IdPlatform = GamePlatform.PC };

            context.Categories.Add(category);
            context.Categories.Add(category2);
            await context.SaveChangesAsync();

            return new { message = "Dados inseridos com sucesso" };
        }
    }
}