using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PIS.Lab4.Models
{
    public class Job
    {
        [Key]
        public int JobID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        [Range(1, 10)]
        public int Priority { get; set; }

        public List<Worker> Workers { get; set; } = new();
    }
}
