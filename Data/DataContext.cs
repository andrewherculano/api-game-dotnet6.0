using GameApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<CategoryGame> Categories { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
    }
}