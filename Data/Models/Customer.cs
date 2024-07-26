using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.Data.Models
{
    public class Customer : EntityBase
    {
        public string? FullName {  get; set; }
        [Required]
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }

    }
}
