using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace Emusic.Models.Policies
{

    public class MusicHandler : AuthorizationHandler <MusicRequirement>
    {
       
            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MusicRequirement requirement)
            {
                //Checking claim for Name....
                if (!context.User.HasClaim(c => c.Type == ClaimTypes.Name))
                {
                    //  
                    return Task.CompletedTask;
                }

                

                //This line allows pulling date of birth from user,  
               // DateTime dateOfBirth = Convert.ToDateTime(context.User
                //    .FindFirst(b => b.Type == ClaimTypes.DateOfBirth).Value);

                //today year minus user input birth year
              //  int userAge = DateTime.Today.Year - dateOfBirth.Year;

                //context.fails->
                return Task.CompletedTask;

              
            }
    }
    
}
