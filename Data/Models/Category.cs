using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.Data.Models
{
    public class Category : EntityBase
    {
        [Required]
        public string Name { get; set; } = null!;

        public int? ParentId { get; set; }

        public Category? Parent { get; set; }
        public List<Category>? SubCategories { get; set;}

    }
}
