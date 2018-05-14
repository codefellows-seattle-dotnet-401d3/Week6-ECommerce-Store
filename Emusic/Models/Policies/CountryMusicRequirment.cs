using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Emusic.Models.Policies
{
    public class CountryMusicRequirment : IAuthorizationRequirement
    {
        public string MusicType { get; set; }

        public CountryMusicRequirment(string country)
        {
            MusicType = country;
        }

    }
}
