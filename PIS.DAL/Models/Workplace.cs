using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS.DAL.Models
{
    public class Workplace
    {
        public int WorkplaceID { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string City { get; set; }
        public List<Worker> Workers { get; set; } = new();
    }
}
