using Microsoft.AspNetCore.Mvc;
using tomiris.utils;

namespace tomiris.Controllers
{
    public class HashController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string data)
        {
            HashGenerate hashStringGenerator = new();
            string hashString = hashStringGenerator.Compute(data);
            ViewData["data"] = data;
            ViewData["hashString"] = hashString;
            return View();
        }
    }
}