using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public State Location { get; set; }
        public DateTime Birthday { get; set; }
    }

    public static class ApplicationRoles
    {
        public static string Admin => "Admin";
        public static string AdminNormalized => Admin.ToUpper();

        public static string Member => "Member";
        public static string MemberNormalized => Member.ToUpper();
    }

    public enum State : Int32
    {
        AL,
        AK,
        AZ,
        AR,
        CA,
        CO,
        CT,
        DC,
        DE,
        FL,
        GA,
        HI,
        ID,
        IL,
        IN,
        IA,
        KS,
        KY,
        LA,
        ME,
        MD,
        MA,
        MI,
        MN,
        MS,
        MO,
        MT,
        NE,
        NV,
        NH,
        NJ,
        NM,
        NY,
        NC,
        ND,
        OH,
        OK,
        OR,
        PA,
        RI,
        SC,
        SD,
        TN,
        TX,
        UT,
        VT,
        VA,
        WA,
        WV,
        WI,
        WY,
        OTHER,
    }
}
