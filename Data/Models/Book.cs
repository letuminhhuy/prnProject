using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.Data.Models
{
    public class Book : EntityBase
    {


        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(20,0)")]


        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }


        [Required]
        public int PublisherId {  get; set; }

        public Category? Category { get; set; }
        public int CategoryId {  get; set; }
        public Author? Author { get; set; }
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(20)]
        public string ISBN { get; set; } = null!;

        [MaxLength(100)]
        public Publisher? Publisher { get; set; }

        public int PublicationYear { get; set; }

        public int NumberOfPage {  get; set; }

        //x - z - y
        public string? BookSize { get; set; }

        public int Stock {  get; set; }


    }
}
