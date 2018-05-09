using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models.Policies
{
    public class StudentRequirement : IAuthorizationRequirement
    {
        public string Student { get; set; }

        public StudentRequirement(string domain)
        {
            Student = domain;
        }
    }
}
