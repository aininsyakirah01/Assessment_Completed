using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data // Ensure this matches the namespace in `TodoController`
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<TodoItem> Todos { get; set; }
    }
}
