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

        

        public IActionResult Index([FromQuery]int next)
        {
            List<Employee> li = db.employee_tb.ToList();
            if (next == 2) {
                PageState.CurrentPage = PageState.CurrentPage+1;
                List<Employee> spel;
                try
                {
                    spel = li.GetRange(PageState.CurrentItem, PageState.ItemPerPage);
                }
                catch (Exception e) {
                    spel = li.GetRange(PageState.CurrentItem, li.Count-PageState.CurrentItem);
                }
                PageState.CurrentItem += PageState.ItemPerPage;

                return View(spel);
            }

            
            PageState.CurrentPage = 1;
            PageState.ItemPerPage = 2;
            PageState.CurrentItem = PageState.ItemPerPage;
            PageState.TotalItems =li.Count;
            PageState.TotalPages = (int)Math.Ceiling((double)PageState.TotalItems/(double)PageState.ItemPerPage);

            List<Employee> speli = li.GetRange(0, PageState.ItemPerPage);
            
            return View(speli);
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
