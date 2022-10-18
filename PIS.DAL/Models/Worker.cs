using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS.DAL.Models
{
    public class Worker
    {
        public int WorkerID { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public bool IsAdmin { get; set; }
        public int WorkplaceID { get; set; }
        public Workplace Workplace { get; set; }
        public List<Job> Jobs { get; set; } = new();
    }
}
