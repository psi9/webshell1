using Microsoft.EntityFrameworkCore;

namespace webshell1
{
    public class CommandContext : DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> options) : base(options)
        {
        }
        public DbSet<Command> Commands { get; set; }
    }
}
