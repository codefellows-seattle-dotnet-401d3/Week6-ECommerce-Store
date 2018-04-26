using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceStore.Models.Polices
{
    /*abstract (blueprints for classes)
     *  Polymorhpism
     *  encapuslation
     *  inheritance
     */

    public class AgeHandler : AuthorizationHandler<AgeRequirment>
    {
        /* This is claims based policy.
         *  
         */

         //context -> handles to authorization
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirment requirement)
        {
            //Checking claim for birthday....
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                //  
                return Task.CompletedTask;
            }

            //This line allows pulling date of birth from user,  
            DateTime dateOfBirth = Convert.ToDateTime(context.User
                .FindFirst(b=> b.Type == ClaimTypes.DateOfBirth).Value);

            //today year minus user input birth year
            int userAge = DateTime.Today.Year - dateOfBirth.Year;

            //context.fails->
            return Task.CompletedTask;

            /*
             * *
             * *
             * *
             * *
             * *
             * *
             */
        }
    }
}
