using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.Data.Models
{
    public class Author
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
    }
}
