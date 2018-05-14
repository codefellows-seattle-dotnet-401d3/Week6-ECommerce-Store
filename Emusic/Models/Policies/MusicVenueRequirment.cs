using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Emusic.Models.Policies
{
    public class MusicVenueRequirment : IAuthorizationRequirement
    {
        public string MusicVenue { get; set; }

        public MusicVenueRequirment(string MusicVenu)
        {
            MusicVenue = MusicVenu;
        }
    }
}
