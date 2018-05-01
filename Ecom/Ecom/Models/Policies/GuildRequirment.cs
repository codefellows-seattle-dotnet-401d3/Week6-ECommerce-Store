using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.Models.Policies
{
    public class GuildRequirment : IAuthorizationRequirement
    {
        public string Guild { get; set; }
        public GuildRequirment(string guild)
        {
            Guild = guild;
        }
    }
}
