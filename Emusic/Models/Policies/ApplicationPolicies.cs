using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;

namespace Emusic.Models.Policies
{
    public static class ApplicationPolicies
    {

        /// <summary>
        /// These are the application Policies 
        /// </summary>

            //Admin Only have access to everything
        public const string AdminOnly = "AdminOnly";
            // Member Only have access to  
        public const string MemberOnly = "MemberOnly";
            //For country music lovers only
        public const string CountryMusicOnly = "CountryMusicOnly";
            //No access to any of the venues
        public const string HeadPhonesOnly = "HeadPhonesOnly";



        /// <summary>
        /// 
        /// </summary>
       
        public static IEnumerable<string> ToEnumerable() =>
          typeof(ApplicationPolicies)
              .GetFields(BindingFlags.Public | BindingFlags.Static |
                         BindingFlags.DeclaredOnly | BindingFlags.FlattenHierarchy)
              .Where(pi => pi.IsLiteral && !pi.IsInitOnly)
              .Select(pi => pi.GetRawConstantValue())
              .Cast<string>();

    }

}
