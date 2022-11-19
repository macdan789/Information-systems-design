using System.Collections.Generic;

namespace PIS.DAL.Models
{
    public class Workplace : BaseEntity
    {
        public int WorkplaceID { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string City { get; set; }
        public List<Worker> Workers { get; set; } = new();
    }
}
