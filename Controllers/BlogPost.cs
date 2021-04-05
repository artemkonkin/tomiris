using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tomiris.Models;
using tomiris.ViewModels;

namespace tomiris.Controllers
{
    public class BlogPost : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Read()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public IActionResult Edit()
        {
            return View();
        }
    }
}