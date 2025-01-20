using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Domain.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }=string.Empty;
        public DateTime CreationDate { get; set; }=DateTime.Now;
        public string CreatedByUserId { get; set; } = string.Empty;
        public string Image { get; set; } =string.Empty;
        public DateTime StartDate { get; set; }
        public int? Duration { get; set; }
        [Column(TypeName = "decimal(18,12)")]
        public decimal Price { get; set; }
        public Category? Category { get; set; }
        [ForeignKey("Category")]

        public Guid Categoryid {  get; set; } 
    }
}
