using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS.Lab4.Models
{
    public class Workplace
    {
        [Key]
        public int WorkplaceID { get; set; }

        [Required]
        [StringLength(20)]
        public string ShortName { get; set; }

        [StringLength(50)]
        public string LongName { get; set; }

        [Required]
        public string City { get; set; }

        public List<Worker> Workers { get; set; } = new();
    }
}
