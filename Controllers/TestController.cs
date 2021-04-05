using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace tomiris.Controllers
{
    public class TestController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            TestFunctions tFun = new();
            tFun.TcDictionary();
            return View();
        }
        
        [Authorize]
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }
    }

    public class TestFunctions : Debugging
    {
        private string value;

        public TestFunctions()
        {

        }

        public int TcDictionary()
        {
            DgMessage("Test Class Dictionary");
            Dictionary<string, string> dicSet = new Dictionary<string, string>();
            dicSet.Add("Namba","Altynay Abdieva");
            dicSet.Add("Ooba","Artem Konkin");
            dicSet.Add("Krista","Begimay Abdieva");
            if(dicSet.TryGetValue("Namba",out value))
            {
                DgMessage($"Namba {value}");
            }
            else
            {
                DgMessage("Not found");
            }
            return 0;
        }
    }

    public class Debugging
    {
        public void DgMessage(string message)
        {
            Debug.WriteLine($"Debug: {message}");
        }
    }
}