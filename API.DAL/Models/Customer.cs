using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DAL.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CustomerId { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string CustomerName { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string Address { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string PostCode { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string City { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string Country { get; set; }
    }
}
