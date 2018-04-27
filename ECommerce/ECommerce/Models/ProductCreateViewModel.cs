using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class ProductCreateViewModel
    {
        [MinLength(3)]
        [Required]
        public string ProductName { get; set; }

        [Required]
        public double ProductPrice { get; set; }

        [MinLength(10)]
        [Required]
        public string ProductDescription { get; set; }

        [MinLength(10)]
        [Required]
        public string ProductImage { get; set; }
    }
}
