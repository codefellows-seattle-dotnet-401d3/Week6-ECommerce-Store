using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class BasketViewModel
    {
        public double Total { get; set; }
        public int TotalItems { get; set; }
        public ICollection<BasketItem> Items { get; set; }
        public IDictionary<long, int> Quantities { get; set; }
    }
}
