using System.ComponentModel.DataAnnotations;

namespace webshell1
{
    public class Command
    {
        public int Id { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
