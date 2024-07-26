using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.Data.Models
{
    public class Publisher : EntityBase
    {
        [Required]
        public string Name {  get; set; }
    }
}
