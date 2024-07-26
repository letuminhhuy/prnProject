using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.Data.Models
{
    public class Sale : EntityBase
    {
        public int? CustomerId { get; set; }
        [Required]
        public int EmployeeId { get; set; }

        public Customer? Customer { get; set; }
        public Employee? Employee { get; set; }

        public List<SaleItem>? SaleDetails {  get; set; }

        [Column(TypeName ="decimal(20,0)")]
        public Decimal TotalPrice {  get; set; }


        
    }
}
