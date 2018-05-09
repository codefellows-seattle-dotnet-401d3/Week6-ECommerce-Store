using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Emusic.Models
{
    /* This model view will only be used for the basekt
     */

    public class BaskeDetailsViewModel
    {
        //This will create the generics for a collection of Items
        public ICollection<BasketItem> Items { get; set; }

        //
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        //
        public IDictionary<long, int> Quantities { get; set; }
        //
        public string ReturnURL { get; set; }

        //These will be used for the Basket Detail; in order to add or subtract items 
        public bool QuantityInputs { get; set; }
        public bool CheckoutButton { get; set; }

    }
}
