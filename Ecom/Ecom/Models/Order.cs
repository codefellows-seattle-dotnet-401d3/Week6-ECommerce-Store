using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public double Total { get; set; }
    }
}