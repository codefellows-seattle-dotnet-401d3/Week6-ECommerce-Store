using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.Models
{
    public class BasketViewModel
    {
        public Basket CurrentBasket { get; set; }
        public List<BasketItem> ItemList { get; set; }
    }
}
