using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models.Policies
{
    public class AdminRequirement : IAuthorizationRequirement
    {
        public bool Admin { get; set; }

        public AdminRequirement(bool admin)
        {
            Admin = admin;
        }
    }
}
