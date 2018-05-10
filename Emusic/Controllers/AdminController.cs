using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


/* For documentation
 *  https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-2.1
 */

namespace Emusic.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
  



}
