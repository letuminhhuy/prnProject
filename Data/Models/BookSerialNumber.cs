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
    public class BookSerialNumber
    {
        public BookSerialNumber() { }

        [Key, ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }
        public String SerialNumber { get; set; }
    }
}
