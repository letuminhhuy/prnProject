using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.Data.Models
{
    public class SaleItem : EntityBase
    {
        [Required]
        public int SaleId {  get; set; }


        public Sale? Sale { get; set; }
        [Required]
        public int quantity { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }



    }
}
