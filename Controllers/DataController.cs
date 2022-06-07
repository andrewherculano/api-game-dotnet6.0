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
            var game = new Game { Id = 1, Title = "Dota", CategoryGame = category2, Description = "Dota é um jogo legal" };
            var game2 = new Game { Id = 2, Title = "Tibia", Description = "Tibia é um jogo legal", CategoryGame = category };
            var user = new User { Id = 1, Username = "andrewtest", Password = "123456", Role = "manager" };
            var user2 = new User { Id = 2, Username = "andrewtest333", Password = "123456", Role = "employee" };

            context.Categories.Add(category);
            context.Categories.Add(category2);
            context.Games.Add(game);
            context.Games.Add(game2);
            context.Users.Add(user);
            context.Users.Add(user2);

            await context.SaveChangesAsync();

            return new { message = "dados cadastrados com sucesso" };
        }
    }
}