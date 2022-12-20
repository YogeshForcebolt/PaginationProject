using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestingPagination.Models;

namespace TestingPagination.Controllers
{
    public class HomeController : Controller
    {
        private EmpDBContext db;

/*        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/
        public HomeController(EmpDBContext dBContext) {
            db = dBContext;
        }

        public IActionResult Index()
        {
            
            List<Employee>  li = db.employee_tb.ToList();
            
            return View(li);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
