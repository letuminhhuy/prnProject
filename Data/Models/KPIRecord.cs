using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.Data.Models
{
    public class KPIRecord : EntityBase
    {
        [Required]
        public int EmployeeId {  get; set; }
        public Employee? Employee { get; set; }
        [Required]
        public int SalesCount {  get; set; }


        [Column(TypeName = "decimal(20,0)")]
        public Decimal TotalSalesAmount { get; set; }
        [Required]
        public int Month {  get; set; }
        [Required]
        public int Year { get; set; }
    }
}
