using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;
using System;

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

        // Словари
        public int TcDictionary()
        {
            DgMessage("Test Class Dictionary");
            Dictionary<string, string> dicSet = new();
            dicSet.Add("Namba", "Altynay Abdieva");
            dicSet.Add("Ooba", "Artem Konkin");
            dicSet.Add("Krista", "Begimay Abdieva");
            if (dicSet.TryGetValue("Namba", out value))
            {
                DgMessage($"Namba {value}");
            }
            else
            {
                DgMessage("Not found");
            }
            return 0;
        }

        // Хеш таблицы
        public void hashTableMethod()
        {
            Hashtable fileExt = new();
            fileExt.Add("gif", "Image");
            fileExt.Add("exe", "Binary file");
            fileExt.Add("cs", "C# source code file");
        }

        // Упаковка, рраспаковка типов
        public void packageAndUnpackTypes()
        {
            int i = 10;
            object number = i; // Упаковать (неявно)
            if (number is int)
            {
                i = (int)number; // Распаковать (явно)
            }
        }

        public void compareObjects()
        {
            Person altynay = new Person { Name = "Altynay", Age = 25 };
            Person artem = new Person { Name = "Altynay", Age = 25 };
            Person begimay = new Person { Name = "Altynay", Age = 25 };

            Person[] myFamily = new Person[] {altynay, artem, begimay};
            Array.Sort(myFamily);

            foreach(Person p in myFamily)
            {
                Debug.Write($"{p.Name} - {p.Age}");
            }
        }
    }

    /// <summary>
    ///  Класс Debug - класс для отладки
    /// </summary>
    /// <remarks>
    /// <para>Этот класс может помочь в отладке кода</para>
    /// </remarks>
    public class Debugging
    {
        /// <summary>
        ///  DgMessage - вывод сообщения в консоль Debug
        /// </summary>
        /// <remarks>
        /// Может вывести сообщение в консоль, принимает параметр с сообщением string message
        /// </remarks>
        /// <returns>
        /// Ничего не возвращает
        /// </returns>
        /// <value>Текст сообщения должен именть тип string</value>
        /// <example>
        /// <code>
        /// <c>DgMessage("Текст сообщения");</c>
        /// </code>
        /// </example>
        /// <param name="message">(string message) - текст сообщения для вывода в консоль</param>
        public void DgMessage(string message)
        {
            Debug.WriteLine($"Debug: {message}");
        }
    }


    class Person : IComparable
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int CompareTo(object obj)
        {
            Person p = obj as Person;
            if (p != null)
                return this.Name.CompareTo(p.Name);
            else
                throw new Exception("Невозможно сравнить два объекта");
        }
    }
}