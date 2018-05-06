using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace Emusic.Models.Policies
{
    public static class ApplicationPolicies
    {
        public const string AdminOnly = "AdminOnly";
        public const string MemberOnly = "MemberOnly";
        public const string CountryMusicOnly = "CountryMusicOnly";


        public static IEnumerable<string> ToEnumerable() =>
          typeof(ApplicationPolicies)
              .GetFields(BindingFlags.Public | BindingFlags.Static |
                         BindingFlags.DeclaredOnly | BindingFlags.FlattenHierarchy)
              .Where(pi => pi.IsLiteral && !pi.IsInitOnly)
              .Select(pi => pi.GetRawConstantValue())
              .Cast<string>();

    }

}
