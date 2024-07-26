using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Management.Data.Models
{
    public class Employee : EntityBase
    {
        public Employee() { }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }

        [Required] public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }

        public string? CitizenId {  get; set; }

        public int? Age { get; set; }
        public string? Address { get; set; }
        public string? ProfileImage {  get; set; }
        public enum Role
        {
            ADMIN =1,
            STAFF =2,
        }
    }
}
