using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerce.Models.Policies
{
    public class StudentHandler : AuthorizationHandler<StudentRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, StudentRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                return Task.CompletedTask;
            }

            string email = context.User.FindFirst(e => e.Type == ClaimTypes.Email).Value;

            if (email.Contains(requirement.Student))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
