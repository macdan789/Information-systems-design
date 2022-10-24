using System.Collections.Generic;

namespace PIS.DAL.Models
{
    public class Job : IEntity
    {
        public int JobID { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public List<Worker> Workers { get; set; } = new();
    }
}
