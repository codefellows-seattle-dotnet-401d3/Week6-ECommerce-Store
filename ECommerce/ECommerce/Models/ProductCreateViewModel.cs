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
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public bool StudentSale { get; set; }

        public double SalePrice { get; set; }

        [MinLength(10)]
        [Required]
        public string Description { get; set; }

        [MinLength(10)]
        [Required]
        public string ImagePath { get; set; }
    }
}

