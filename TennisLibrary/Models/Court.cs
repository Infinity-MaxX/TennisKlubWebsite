using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLibrary.Models
{
    public class Court
    {
        public string Name { get; set; }
        public string Type {  get; set; }
        public DateOnly LastMaintenance {  get; set; }
    }
}
