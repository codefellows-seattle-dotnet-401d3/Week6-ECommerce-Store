using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Emusic.Models
{
    public class Product
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string ImageHref { get; set; }

        [Display(Name = "Policy??")]
        public string PolicyRequirement { get; set; }


    }
}
