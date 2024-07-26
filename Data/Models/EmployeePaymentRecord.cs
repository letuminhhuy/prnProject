using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.Data.Models
{
    public class EmployeePaymentRecord : EntityBase
    {

        public EmployeePaymentRecord() { }

        public int EmployeeId { get; set; } 
        
        public int Month {  get; set; }
        public int Year { get; set; }

        //Payed / not payed
        public int Status { get; set; }
    }
}
