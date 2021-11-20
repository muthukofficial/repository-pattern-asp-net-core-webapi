using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DAL.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string ProductName { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; }
    }
}
