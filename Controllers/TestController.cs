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
    public class TestController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public TestController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }


    public struct KeyAndValue
    {
        public string Key;
        public int Value;
    }

    abstract class UserAbsClass
    {
        public string Age;
    }

    static class ArrayMethods
    {
        private static int [] Add(int[] array)
        {
            return array;
        }

        public static void InitClass()
        {
            int[] a = new int[] {1,2,3,4};
            Add(a);
        }
    }



//namespace
}