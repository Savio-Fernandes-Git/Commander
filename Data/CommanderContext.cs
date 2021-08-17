using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        {
            
        }

        //this bssically means we want to represent our command object down to our db as a DbSet
        public DbSet<Command> Commands { get; set; }
    }
}