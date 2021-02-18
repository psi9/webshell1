using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webshell1.Controllers
{
    public class CommandsController
    {
        private readonly CommandContext context;
        public CommandsController(CommandContext context)
        {
            this.context = context;
        }
        public async Task<ActionResult<List<Command>>> Index()
        {
            return await context.Commands.ToListAsync();
        }
    }
}
