using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WhatsUp.API.Controllers
{
    public class EmailController : Controller
    {
        public async Task<IActionResult> VerifyEmail(string userId, string code)
        {
            return View();
        }
    }
}
