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

        public IActionResult Main() {
            return View();        
        }
        

        public IActionResult Index([FromQuery]string next, [FromQuery] string previous, [FromQuery] int page,[FromQuery]int itemPerPage)
        {
            
            if (next == "next") {
                if (PageState.TotalPages == PageState.CurrentPage)
                {
                    List<Employee> last = db.employee_tb.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;
                    return Ok(last);
                }
                PageState.CurrentPage = PageState.CurrentPage + 1;
                PageState.PageNumberState();
                ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;
                PageState.CurrentItem += PageState.ItemPerPage;
                List<Employee> nex = db.employee_tb.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                return Ok(nex);
            }

            if (previous == "previous") {
                
                if (1 == PageState.CurrentPage)
                {
                    List<Employee> prev1 = db.employee_tb.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;
                    return Ok(prev1);
                }
                PageState.CurrentPage = PageState.CurrentPage - 1;
                PageState.PageNumberState();
                ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;
                PageState.CurrentItem -= PageState.ItemPerPage;
                List<Employee> prev = db.employee_tb.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                
                return Ok();
            }

            if (page > 0) {
                if (PageState.CurrentPage == page) {
                    ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;
                    List<Employee> same = db.employee_tb.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    return Ok(same);
                }
                PageState.CurrentPage = page;
                int skip = (PageState.CurrentPage-1)*PageState.ItemPerPage;
                PageState.CurrentItem =skip;
                List<Employee> defined = db.employee_tb.Skip(skip).Take(PageState.ItemPerPage).ToList();
                PageState.PageNumberState();
                ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;
                return Ok(defined);
            }

            List<Employee> li = db.employee_tb.ToList();
            PageState.CurrentPage = 1;
            PageState.ItemPerPage = itemPerPage==0?2:itemPerPage;
            PageState.CurrentItem = 0;
            PageState.TotalItems =li.Count;
            PageState.TotalPages = (int)Math.Ceiling((double)PageState.TotalItems/(double)PageState.ItemPerPage);
            PageState.PageNumberState();
            ViewData["startPage"] = PageState.StartPage;
            ViewData["endPage"] = PageState.EndPage;
            List<Employee> data = db.employee_tb.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
            JsonData frontData = new JsonData(data, PageState.StartPage, PageState.EndPage);
            return Ok(frontData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="previous"></param>
        /// <param name="page"></param>
        /// <param name="itemPerPage"></param>
        /// <param name="search"></param>
        /// <returns></returns>



        //Search Operation
        public IActionResult Search([FromQuery] string next, [FromQuery] string previous, [FromQuery] int page, [FromQuery] int itemPerPage, [FromQuery] string search)
        {
            if (next == "next")
            {
                if (PageState.TotalPages == PageState.CurrentPage)
                {
                    List<Employee> last = (db.employee_tb).Where(elem => elem.Name.Contains(search)).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;
                    return View(last);
                }
                PageState.CurrentPage = PageState.CurrentPage + 1;
                PageState.PageNumberState();
                ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;
                PageState.CurrentItem += PageState.ItemPerPage;
                List<Employee> nex = (db.employee_tb).Where(elem => elem.Name.Contains(search)).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                return View(nex);
            }

            if (previous == "previous")
            {

                if (1 == PageState.CurrentPage)
                {
                    List<Employee> prev1 = (db.employee_tb).Where(elem => elem.Name.Contains(search)).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;
                    return View(prev1);
                }
                PageState.CurrentPage = PageState.CurrentPage - 1;
                PageState.PageNumberState();
                ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;
                PageState.CurrentItem -= PageState.ItemPerPage;
                List<Employee> prev = (db.employee_tb).Where(elem => elem.Name.Contains(search)).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();

                return View(prev);
            }

            if (page > 0)
            {
                if (PageState.CurrentPage == page)
                {
                    ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;
                    List<Employee> same = (db.employee_tb).Where(elem => elem.Name.Contains(search)).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    return View(same);
                }
                PageState.CurrentPage = page;
                int skip = (PageState.CurrentPage - 1) * PageState.ItemPerPage;
                PageState.CurrentItem = skip;
                List<Employee> defined = (db.employee_tb).Where(elem => elem.Name.Contains(search)).Skip(skip).Take(PageState.ItemPerPage).ToList();
                PageState.PageNumberState();
                ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;
                return View(defined);
            }

                List<Employee> searchedData = (db.employee_tb).Where(elem => elem.Name.Contains(search)).ToList();
                PageState.CurrentPage = 1;
                PageState.ItemPerPage = itemPerPage == 0 ? 2 : itemPerPage;
                PageState.CurrentItem = 0;
                PageState.TotalItems = searchedData.Count;
                PageState.TotalPages = (int)Math.Ceiling((double)PageState.TotalItems / (double)PageState.ItemPerPage);
                PageState.PageNumberState();
                ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;
                return View(searchedData.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage));
            
        }







        public IActionResult FilterByAscending([FromQuery] string next, [FromQuery] string previous, [FromQuery] int page, [FromQuery] int itemPerPage) {
             if (next == "next")
            {
                if (PageState.TotalPages == PageState.CurrentPage)
                {
                    List<Employee> last = (db.employee_tb).OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;
                    return View(last);
                }
                PageState.CurrentPage = PageState.CurrentPage + 1;
                PageState.PageNumberState();
                ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;
                PageState.CurrentItem += PageState.ItemPerPage;
                List<Employee> nex = (db.employee_tb).OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                return View(nex);
            }

            if (previous == "previous")
            {

                if (1 == PageState.CurrentPage)
                {
                    List<Employee> prev1 = (db.employee_tb).OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;
                    return View(prev1);
                }
                PageState.CurrentPage = PageState.CurrentPage - 1;
                PageState.PageNumberState();
                ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;
                PageState.CurrentItem -= PageState.ItemPerPage;
                List<Employee> prev = (db.employee_tb).OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();

                return View(prev);
            }

            if (page > 0)
            {
                if (PageState.CurrentPage == page)
                {
                    ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;
                    List<Employee> same = (db.employee_tb).OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    return View(same);
                }
                PageState.CurrentPage = page;
                int skip = (PageState.CurrentPage - 1) * PageState.ItemPerPage;
                PageState.CurrentItem = skip;
                List<Employee> defined = (db.employee_tb).OrderBy(elem => elem.Name).Skip(skip).Take(PageState.ItemPerPage).ToList();
                PageState.PageNumberState();
                ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;
                return View(defined);
            }

                List<Employee> searchedData = (db.employee_tb).OrderBy(elem=>elem.Name).ToList();
                PageState.CurrentPage = 1;
                PageState.ItemPerPage = itemPerPage == 0 ? 2 : itemPerPage;
                PageState.CurrentItem = 0;
                PageState.TotalItems = searchedData.Count;
                PageState.TotalPages = (int)Math.Ceiling((double)PageState.TotalItems / (double)PageState.ItemPerPage);
                PageState.PageNumberState();
                ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;
                return View(searchedData.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage));
        }

        public IActionResult FilterByDescending([FromQuery] string next, [FromQuery] string previous, [FromQuery] int page, [FromQuery] int itemPerPage)
        {
            if (next == "next")
            {
                if (PageState.TotalPages == PageState.CurrentPage)
                {
                    List<Employee> last = (db.employee_tb).OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;
                    return View(last);
                }
                PageState.CurrentPage = PageState.CurrentPage + 1;
                PageState.PageNumberState();
                ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;
                PageState.CurrentItem += PageState.ItemPerPage;
                List<Employee> nex = (db.employee_tb).OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                return View(nex);
            }

            if (previous == "previous")
            {

                if (1 == PageState.CurrentPage)
                {
                    List<Employee> prev1 = (db.employee_tb).OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;
                    return View(prev1);
                }
                PageState.CurrentPage = PageState.CurrentPage - 1;
                PageState.PageNumberState();
                ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;
                PageState.CurrentItem -= PageState.ItemPerPage;
                List<Employee> prev = (db.employee_tb).OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();

                return View(prev);
            }

            if (page > 0)
            {
                if (PageState.CurrentPage == page)
                {
                    ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;
                    List<Employee> same = (db.employee_tb).OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    return View(same);
                }
                PageState.CurrentPage = page;
                int skip = (PageState.CurrentPage - 1) * PageState.ItemPerPage;
                PageState.CurrentItem = skip;
                List<Employee> defined = (db.employee_tb).OrderByDescending(elem => elem.Name).Skip(skip).Take(PageState.ItemPerPage).ToList();
                PageState.PageNumberState();
                ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;
                return View(defined);
            }

            List<Employee> searchedData = (db.employee_tb).OrderByDescending(elem => elem.Name).ToList();
            PageState.CurrentPage = 1;
            PageState.ItemPerPage = itemPerPage == 0 ? 2 : itemPerPage;
            PageState.CurrentItem = 0;
            PageState.TotalItems = searchedData.Count;
            PageState.TotalPages = (int)Math.Ceiling((double)PageState.TotalItems / (double)PageState.ItemPerPage);
            PageState.PageNumberState();
            ViewData["startPage"] = PageState.StartPage;
            ViewData["endPage"] = PageState.EndPage;
            return View(searchedData.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
