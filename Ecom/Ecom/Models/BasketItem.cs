using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public Basket Basket { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}