using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceStore.Controllers
{
    //If user has access this controller
    /* In order to make a policy
     * 
     */

    //[Authorize(Policy ="AdminOnly")]
    public class AdminController : Controller 
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
