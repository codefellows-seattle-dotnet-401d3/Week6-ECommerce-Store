using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecom.Models.Policies
{
    public class GuildPolicyHandler : AuthorizationHandler<GuildRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GuildRequirment requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "guildCheck"))
            {
                return Task.CompletedTask;
            }

            if (context.User.HasClaim(c => c.Type == "guildCheck"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
