using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Emusic.Models
{
    public class BasketAdderViewModel
    {
        public long ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Url]
        public string ReturnUrl { get; set; }
    }
}
