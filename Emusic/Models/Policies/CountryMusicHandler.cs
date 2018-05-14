using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;



namespace Emusic.Models.Policies
{
    public class CountryMusicHandler : AuthorizationHandler<CountryMusicRequirment>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CountryMusicRequirment requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "Country"))
            {
                return Task.CompletedTask;
            }

            if (context.User.HasClaim(c => c.Type == "Country"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }
}
