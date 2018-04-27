using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models.Policies
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; set; }

        public MinimumAgeRequirement(int age)
        {
            MinimumAge = age;
        }
    }
}
