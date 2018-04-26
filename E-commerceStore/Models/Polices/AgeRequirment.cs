using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceStore.Models.Polices
{

    /* Brings in a h
     */
    public class AgeRequirment : IAuthorizationRequirement
    {
        //properties
        public int MinimumAge { get; set; }

        public AgeRequirment()
        {

        }


    }
}
