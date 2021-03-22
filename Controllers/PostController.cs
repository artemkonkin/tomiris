using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tomiris.Models;

namespace tomiris.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index () {
            return View();
        }
        
        public IActionResult Post(){
            return View();
        }
    }
}