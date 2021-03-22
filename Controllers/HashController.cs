using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

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
            string hashString = GetHashData(data);
            ViewData["data"] = data;
            ViewData["hashString"] = hashString;
            return View();
        }

        static string GetHashData(string data)
        {
            ASCIIEncoding encoding = new();

            if (String.IsNullOrWhiteSpace(data))
            {
                return "Введите значение";
            }
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(encoding.GetBytes(data));

                StringBuilder builder = new();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }

        }
    }
}