using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
        public double Total { get; set; }
        public bool CheckedOut { get; set; }
    }
}