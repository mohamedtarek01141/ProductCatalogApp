   using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecpmmerceApp.Domain.Entities
{
    public class ArchivePayment
    {
        public Guid Id { get; set; } 
        public Guid UserId { get; set; } 
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } 
          public DateTime CreatedDate { get; set; } = DateTime.UtcNow; 
    }
}
