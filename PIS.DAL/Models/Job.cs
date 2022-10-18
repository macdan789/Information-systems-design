using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS.DAL.Models
{
    public class Job
    {
        public int JobID { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public List<Worker> Workers { get; set; } = new();
    }
}
