using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.Models.Policies
{
    public class ClassRequirment : IAuthorizationRequirement
    {
        public string Class { get; set; }
        public ClassRequirment(string charClass)
        {
            Class = charClass;
        }
    }
}
