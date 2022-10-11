using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS.Lab4.Models
{
    public class Worker
    {
        [Key]
        public int WorkerID { get; set; }

        [Required]
        public string Name { get; set; }
        
        public bool IsAdmin { get; set; }

        public int WorkplaceID { get; set; }
        public Workplace Workplace { get; set; }
        
        public List<Job> Jobs { get; set; } = new();
    }
}
