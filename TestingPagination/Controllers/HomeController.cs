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

        private static int state;
        public IActionResult Index([FromQuery] string next, [FromQuery] string previous, [FromQuery] int page, [FromForm] int itemPerPage, [FromQuery] string sort,[FromQuery] string gender,[FromQuery] string status,[FromQuery] string filterCreated, [FromQuery] DateTime createdToDate, [FromQuery] DateTime createdFromDate, [FromQuery] string filterModified, [FromQuery] DateTime modifiedToDate, [FromQuery] DateTime modifiedFromDate)
        {
            if (sort!=null) {
                if (state == 2)
                {
                    state = 0;
                }
                else {
                    state++;
                }
            }

            if (state == 1)
            {
                if (next == "next")
                {
                    if (PageState.TotalPages == PageState.CurrentPage)
                    {
                        List<Employee> last = (db.employee_tb).ToList();
                        PageState.PageNumberState();
                        if (gender != null && gender.ToLower() == "male")
                        {
                            last = last.Where(li => li.Gender == "Male").ToList();

                        }
                        else if (gender != null && gender.ToLower() == "female")
                        {
                            last = last.Where(li => li.Gender == "Female").ToList();
                        }
                        if (status != null && status.ToLower() == "active")
                        {
                            last = last.Where(li => li.Status == true).ToList();
                        }
                        else if (status != null && status.ToLower() == "inactive")
                        {
                            last = last.Where(li => li.Status == false).ToList();
                        }
                        if (status != null && status.ToLower() == "active")
                        {
                            last = last.Where(li => li.Status == true).ToList();
                        }
                        else if (status != null && status.ToLower() == "inactive")
                        {
                            last = last.Where(li => li.Status == false).ToList();
                        }
                        if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                        {
                            last = last.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                        }
                        if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                        {
                            last = last.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                        }
                        last = last.OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                        JsonData lastData = new JsonData(last, PageState.StartPage, PageState.EndPage);
                        return Ok(lastData);

                    }
                    PageState.CurrentPage = PageState.CurrentPage + 1;
                    PageState.PageNumberState();
                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage; */
                    PageState.CurrentItem += PageState.ItemPerPage;
                    List<Employee> nex = (db.employee_tb).ToList();
                    if (gender != null && gender.ToLower() == "male")
                    {
                        nex = nex.Where(li => li.Gender == "Male").ToList();

                    }
                    else if (gender != null && gender.ToLower() == "female")
                    {
                        nex = nex.Where(li => li.Gender == "Female").ToList();
                    }
                    if (status != null && status.ToLower() == "active")
                    {
                        nex = nex.Where(li => li.Status == true).ToList();
                    }
                    else if (status != null && status.ToLower() == "inactive")
                    {
                        nex = nex.Where(li => li.Status == false).ToList();
                    }
                    if (status != null && status.ToLower() == "active")
                    {
                        nex = nex.Where(li => li.Status == true).ToList();
                    }
                    else if (status != null && status.ToLower() == "inactive")
                    {
                        nex = nex.Where(li => li.Status == false).ToList();
                    }
                    if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        nex = nex.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                    }
                    if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        nex = nex.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                    }
                    nex = nex.OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    JsonData lastData1 = new JsonData(nex, PageState.StartPage, PageState.EndPage);
                    return Ok(lastData1);
                }

                if (previous == "previous")
                {

                    if (1 == PageState.CurrentPage)
                    {
                        List<Employee> prev1 = (db.employee_tb).ToList();
                        PageState.PageNumberState();
                        /*ViewData["startPage"] = PageState.StartPage;
                        ViewData["endPage"] = PageState.EndPage; */
                        if (gender != null && gender.ToLower() == "male")
                        {
                            prev1 = prev1.Where(li => li.Gender == "Male").ToList();

                        }
                        else if (gender != null && gender.ToLower() == "female")
                        {
                            prev1 = prev1.Where(li => li.Gender == "Female").ToList();
                        }
                        if (status != null && status.ToLower() == "active")
                        {
                            prev1 = prev1.Where(li => li.Status == true).ToList();
                        }
                        else if (status != null && status.ToLower() == "inactive")
                        {
                            prev1 = prev1.Where(li => li.Status == false).ToList();
                        }
                        if (status != null && status.ToLower() == "active")
                        {
                            prev1 = prev1.Where(li => li.Status == true).ToList();
                        }
                        else if (status != null && status.ToLower() == "inactive")
                        {
                            prev1 = prev1.Where(li => li.Status == false).ToList();
                        }
                        if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                        {
                            prev1 = prev1.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                        }
                        if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                        {
                            prev1 = prev1.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                        }
                        prev1 = prev1.OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                        JsonData previousData1 = new JsonData(prev1, PageState.StartPage, PageState.EndPage);
                        return Ok(previousData1);
                    }
                    PageState.CurrentPage = PageState.CurrentPage - 1;
                    PageState.PageNumberState();
                    /* ViewData["startPage"] = PageState.StartPage;
                     ViewData["endPage"] = PageState.EndPage; */
                    PageState.CurrentItem -= PageState.ItemPerPage;
                    List<Employee> prev = (db.employee_tb).ToList();
                    if (gender != null && gender.ToLower() == "male")
                    {
                        prev = prev.Where(li => li.Gender == "Male").ToList();

                    }
                    else if (gender != null && gender.ToLower() == "female")
                    {
                        prev = prev.Where(li => li.Gender == "Female").ToList();
                    }
                    if (status != null && status.ToLower() == "active")
                    {
                        prev = prev.Where(li => li.Status == true).ToList();
                    }
                    else if (status != null && status.ToLower() == "inactive")
                    {
                        prev = prev.Where(li => li.Status == false).ToList();
                    }
                    if (status != null && status.ToLower() == "active")
                    {
                        prev = prev.Where(li => li.Status == true).ToList();
                    }
                    else if (status != null && status.ToLower() == "inactive")
                    {
                        prev = prev.Where(li => li.Status == false).ToList();
                    }
                    if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        prev = prev.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                    }
                    if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        prev = prev.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                    }
                    prev = prev.OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    JsonData previousData = new JsonData(prev, PageState.StartPage, PageState.EndPage);
                    return Ok(previousData);
                }

                if (page > 0)
                {
                    if (PageState.CurrentPage == page)
                    {
                        /*ViewData["startPage"] = PageState.StartPage;
                        ViewData["endPage"] = PageState.EndPage; */
                        List<Employee> same = (db.employee_tb).ToList();
                        if (gender != null && gender.ToLower() == "male")
                        {
                            same = same.Where(li => li.Gender == "Male").ToList();

                        }
                        else if (gender != null && gender.ToLower() == "female")
                        {
                            same = same.Where(li => li.Gender == "Female").ToList();
                        }
                        if (status != null && status.ToLower() == "active")
                        {
                            same = same.Where(li => li.Status == true).ToList();
                        }
                        else if (status != null && status.ToLower() == "inactive")
                        {
                            same = same.Where(li => li.Status == false).ToList();
                        }
                        if (status != null && status.ToLower() == "active")
                        {
                            same = same.Where(li => li.Status == true).ToList();
                        }
                        else if (status != null && status.ToLower() == "inactive")
                        {
                            same = same.Where(li => li.Status == false).ToList();
                        }
                        if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                        {
                            same = same.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                        }
                        if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                        {
                            same = same.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                        }
                        same = same.OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                        JsonData sameData = new JsonData(same, PageState.StartPage, PageState.EndPage);
                        return Ok(sameData);
                    }
                    PageState.CurrentPage = page;
                    int skip = (PageState.CurrentPage - 1) * PageState.ItemPerPage;
                    PageState.CurrentItem = skip;
                    List<Employee> defined = (db.employee_tb).ToList();
                    
                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage; */
                    if (gender != null && gender.ToLower() == "male")
                    {
                        defined = defined.Where(li => li.Gender == "Male").ToList();

                    }
                    else if (gender != null && gender.ToLower() == "female")
                    {
                        defined = defined.Where(li => li.Gender == "Female").ToList();
                    }
                    if (status != null && status.ToLower() == "active")
                    {
                        defined = defined.Where(li => li.Status == true).ToList();
                    }
                    else if (status != null && status.ToLower() == "inactive")
                    {
                        defined = defined.Where(li => li.Status == false).ToList();
                    }
                    if (status != null && status.ToLower() == "active")
                    {
                        defined = defined.Where(li => li.Status == true).ToList();
                    }
                    else if (status != null && status.ToLower() == "inactive")
                    {
                        defined = defined.Where(li => li.Status == false).ToList();
                    }
                    if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        defined = defined.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                    }
                    if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        defined = defined.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                    }
                    defined = defined.OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();

                    PageState.PageNumberState();
                    JsonData definedData = new JsonData(defined, PageState.StartPage, PageState.EndPage);
                    return Ok(definedData);
                }

                List<Employee> searchedData = (db.employee_tb).ToList();
                PageState.CurrentPage = 1;
                if (gender != null && gender.ToLower() == "male")
                {
                    searchedData = searchedData.Where(li => li.Gender == "Male").ToList();

                }
                else if (gender != null && gender.ToLower() == "female")
                {
                    searchedData = searchedData.Where(li => li.Gender == "Female").ToList();
                }
                if (status != null && status.ToLower() == "active")
                {
                    searchedData = searchedData.Where(li => li.Status == true).ToList();
                }
                else if (status != null && status.ToLower() == "inactive")
                {
                    searchedData = searchedData.Where(li => li.Status == false).ToList();
                }
                if (status != null && status.ToLower() == "active")
                {
                    searchedData = searchedData.Where(li => li.Status == true).ToList();
                }
                else if (status != null && status.ToLower() == "inactive")
                {
                    searchedData = searchedData.Where(li => li.Status == false).ToList();
                }
                if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                {
                    searchedData = searchedData.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                }
                if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                {
                    searchedData = searchedData.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                }
                searchedData = searchedData.OrderBy(elem => elem.Name).ToList();
                //PageState.ItemPerPage = itemPerPage == 0 ? 2 : itemPerPage;
                PageState.ItemPerPage = itemPerPage != 0 ? itemPerPage : PageState.ItemPerPage;
                PageState.CurrentItem = 0;
                PageState.TotalItems = searchedData.Count;
                PageState.TotalPages = (int)Math.Ceiling((double)PageState.TotalItems / (double)PageState.ItemPerPage);
                PageState.PageNumberState();
                /*ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage; */
                JsonData frontData1 = new JsonData(searchedData.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList(), PageState.StartPage, PageState.EndPage);
                return Ok(frontData1);
            }

            else if (state == 2) {
                if (next == "next")
                {
                    if (PageState.TotalPages == PageState.CurrentPage)
                    {
                        List<Employee> last = (db.employee_tb).ToList();

                        /*ViewData["startPage"] = PageState.StartPage;
                        ViewData["endPage"] = PageState.EndPage; */
                        if (gender != null && gender.ToLower() == "male")
                        {
                            last = last.Where(li => li.Gender == "Male").ToList();

                        }
                        else if (gender != null && gender.ToLower() == "female")
                        {
                            last = last.Where(li => li.Gender == "Female").ToList();
                        }
                        if (status != null && status.ToLower() == "active")
                        {
                            last = last.Where(li => li.Status == true).ToList();
                        }
                        else if (status != null && status.ToLower() == "inactive")
                        {
                            last = last.Where(li => li.Status == false).ToList();
                        }
                        if (status != null && status.ToLower() == "active")
                        {
                            last = last.Where(li => li.Status == true).ToList();
                        }
                        else if (status != null && status.ToLower() == "inactive")
                        {
                            last = last.Where(li => li.Status == false).ToList();
                        }
                        if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                        {
                            last = last.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                        }
                        if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                        {
                            last = last.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                        }
                        last = last.OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                        PageState.PageNumberState();
                        JsonData lastData = new JsonData(last, PageState.StartPage, PageState.EndPage);
                        return Ok(lastData);

                    }
                    PageState.CurrentPage = PageState.CurrentPage + 1;
                    
                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage; */
                     PageState.CurrentItem += PageState.ItemPerPage;
                    List<Employee> nex = (db.employee_tb).ToList();
                    if (gender != null && gender.ToLower() == "male")
                    {
                        nex = nex.Where(li => li.Gender == "Male").ToList();

                    }
                    else if (gender != null && gender.ToLower() == "female")
                    {
                        nex = nex.Where(li => li.Gender == "Female").ToList();
                    }
                    if (status != null && status.ToLower() == "active")
                    {
                        nex = nex.Where(li => li.Status == true).ToList();
                    }
                    else if (status != null && status.ToLower() == "inactive")
                    {
                        nex = nex.Where(li => li.Status == false).ToList();
                    }
                    if (status != null && status.ToLower() == "active")
                    {
                        nex = nex.Where(li => li.Status == true).ToList();
                    }
                    else if (status != null && status.ToLower() == "inactive")
                    {
                        nex = nex.Where(li => li.Status == false).ToList();
                    }
                    if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        nex = nex.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                    }
                    if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        nex = nex.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                    }
                    nex = nex.OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    JsonData lastData1 = new JsonData(nex, PageState.StartPage, PageState.EndPage);
                    return Ok(lastData1);
                }

                if (previous == "previous")
                {

                    if (1 == PageState.CurrentPage)
                    {
                        List<Employee> prev1 = (db.employee_tb).ToList();

                        /*ViewData["startPage"] = PageState.StartPage;
                        ViewData["endPage"] = PageState.EndPage; */
                        if (gender != null && gender.ToLower() == "male")
                        {
                            prev1 = prev1.Where(li => li.Gender == "Male").ToList();

                        }
                        else if (gender != null && gender.ToLower() == "female")
                        {
                            prev1 = prev1.Where(li => li.Gender == "Female").ToList();
                        }
                        if (status != null && status.ToLower() == "active")
                        {
                            prev1 = prev1.Where(li => li.Status == true).ToList();
                        }
                        else if (status != null && status.ToLower() == "inactive")
                        {
                            prev1 = prev1.Where(li => li.Status == false).ToList();
                        }
                        if (status != null && status.ToLower() == "active")
                        {
                            prev1 = prev1.Where(li => li.Status == true).ToList();
                        }
                        else if (status != null && status.ToLower() == "inactive")
                        {
                            prev1 = prev1.Where(li => li.Status == false).ToList();
                        }
                        if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                        {
                            prev1 = prev1.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                        }
                        if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                        {
                            prev1 = prev1.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                        }

                        prev1 = prev1.OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                        PageState.PageNumberState();
                        JsonData previousData1 = new JsonData(prev1, PageState.StartPage, PageState.EndPage);
                        return Ok(previousData1);
                    }
                    PageState.CurrentPage = PageState.CurrentPage - 1;
                   

                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage; */
                     PageState.CurrentItem -= PageState.ItemPerPage;
                    List<Employee> prev = (db.employee_tb).ToList();
                    if (gender != null && gender.ToLower() == "male")
                    {
                        prev = prev.Where(li => li.Gender == "Male").ToList();

                    }
                    else if (gender != null && gender.ToLower() == "female")
                    {
                        prev = prev.Where(li => li.Gender == "Female").ToList();
                    }
                    if (status != null && status.ToLower() == "active")
                    {
                        prev = prev.Where(li => li.Status == true).ToList();
                    }
                    else if (status != null && status.ToLower() == "inactive")
                    {
                        prev = prev.Where(li => li.Status == false).ToList();
                    }
                    if (status != null && status.ToLower() == "active")
                    {
                        prev = prev.Where(li => li.Status == true).ToList();
                    }
                    else if (status != null && status.ToLower() == "inactive")
                    {
                        prev = prev.Where(li => li.Status == false).ToList();
                    }
                    if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        prev = prev.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                    }
                    if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        prev = prev.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                    }

                    prev = prev.OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();

                    PageState.PageNumberState();
                    JsonData previousData = new JsonData(prev, PageState.StartPage, PageState.EndPage);
                    return Ok(previousData);
                }

                if (page > 0)
                {
                    if (PageState.CurrentPage == page)
                    {
                        /*ViewData["startPage"] = PageState.StartPage;
                        ViewData["endPage"] = PageState.EndPage; */
                         List < Employee > same = (db.employee_tb).ToList();
                        if (gender != null && gender.ToLower() == "male")
                        {
                            same = same.Where(li => li.Gender == "Male").ToList();

                        }
                        else if (gender != null && gender.ToLower() == "female")
                        {
                            same = same.Where(li => li.Gender == "Female").ToList();
                        }
                        if (status != null && status.ToLower() == "active")
                        {
                            same = same.Where(li => li.Status == true).ToList();
                        }
                        else if (status != null && status.ToLower() == "inactive")
                        {
                            same = same.Where(li => li.Status == false).ToList();
                        }
                        if (status != null && status.ToLower() == "active")
                        {
                            same = same.Where(li => li.Status == true).ToList();
                        }
                        else if (status != null && status.ToLower() == "inactive")
                        {
                            same = same.Where(li => li.Status == false).ToList();
                        }
                        if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                        {
                            same = same.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                        }
                        if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                        {
                            same = same.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                        }

                        same = same.OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                        PageState.PageNumberState();
                        JsonData sameData = new JsonData(same, PageState.StartPage, PageState.EndPage);
                        return Ok(sameData);
                    }
                    PageState.CurrentPage = page;
                    int skip = (PageState.CurrentPage - 1) * PageState.ItemPerPage;
                    PageState.CurrentItem = skip;
                    List<Employee> defined = (db.employee_tb).ToList();

                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage; */
                    if (gender != null && gender.ToLower() == "male")
                    {
                        defined = defined.Where(li => li.Gender == "Male").ToList();

                    }
                    else if (gender != null && gender.ToLower() == "female")
                    {
                        defined = defined.Where(li => li.Gender == "Female").ToList();
                    }
                    if (status != null && status.ToLower() == "active")
                    {
                        defined = defined.Where(li => li.Status == true).ToList();
                    }
                    else if (status != null && status.ToLower() == "inactive")
                    {
                        defined = defined.Where(li => li.Status == false).ToList();
                    }
                    if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        defined = defined.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                    }
                    if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        defined = defined.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                    }

                    defined = defined.OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();

                    PageState.PageNumberState();
                    JsonData definedData = new JsonData(defined, PageState.StartPage, PageState.EndPage);
                    return Ok(definedData);
                }



                List<Employee> searchedData = (db.employee_tb).ToList();
                
                if (gender != null && gender.ToLower() == "male")
                {
                    searchedData = searchedData.Where(li => li.Gender == "Male").ToList();

                }
                else if (gender != null && gender.ToLower() == "female")
                {
                    searchedData = searchedData.Where(li => li.Gender == "Female").ToList();
                }
                if (status != null && status.ToLower() == "active")
                {
                    searchedData = searchedData.Where(li => li.Status == true).ToList();
                }
                else if (status != null && status.ToLower() == "inactive")
                {
                    searchedData = searchedData.Where(li => li.Status == false).ToList();
                }
                if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                {
                    searchedData = searchedData.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                }
                if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                {
                    searchedData = searchedData.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                }

                searchedData = searchedData.OrderByDescending(elem => elem.Name).ToList();

                PageState.CurrentPage = 1;
                //PageState.ItemPerPage = itemPerPage == 0 ? 2 : itemPerPage;
                PageState.ItemPerPage = itemPerPage != 0 ? itemPerPage : PageState.ItemPerPage;
                PageState.CurrentItem = 0;
                PageState.TotalItems = searchedData.Count;
                PageState.TotalPages = (int)Math.Ceiling((double)PageState.TotalItems / (double)PageState.ItemPerPage);
                PageState.PageNumberState();
                /*ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage; */
                 JsonData frontData1 = new JsonData(searchedData.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList(), PageState.StartPage, PageState.EndPage);
                return Ok(frontData1);
            }
            ///
            ///default data
            ///



            if (next == "next") {
                if (PageState.TotalPages == PageState.CurrentPage)
                {
                    List<Employee> last = db.employee_tb.ToList();

                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;*/
                    if (gender != null && gender.ToLower() == "male")
                    {
                        last = last.Where(li => li.Gender == "Male").ToList();
                    }
                    else if (gender != null && gender.ToLower() == "female")
                    {
                        last = last.Where(li => li.Gender == "Female").ToList();
                    }
                    if (status != null && status.ToLower() == "active")
                    {
                        last = last.Where(li => li.Status == true).ToList();
                    }
                    else if (status != null && status.ToLower() == "inactive")
                    {
                        last = last.Where(li => li.Status == false).ToList();
                    }
                    if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        last = last.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                    }
                    if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        last = last.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                    }
                    last = last.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    JsonData lastData = new JsonData(last, PageState.StartPage, PageState.EndPage);
                    return Ok(lastData);
                }
                PageState.CurrentPage = PageState.CurrentPage + 1;
                
                /*ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;*/
                PageState.CurrentItem += PageState.ItemPerPage;
                List<Employee> nex = db.employee_tb.ToList();
                if (gender != null && gender.ToLower() == "male")
                {
                    nex = nex.Where(li => li.Gender == "Male").ToList();
                }
                else if (gender != null && gender.ToLower() == "female")
                {
                    nex = nex.Where(li => li.Gender == "Female").ToList();
                }
                if (status != null && status.ToLower() == "active")
                {
                    nex = nex.Where(li => li.Status == true).ToList();
                }
                else if (status != null && status.ToLower() == "inactive")
                {
                    nex = nex.Where(li => li.Status == false).ToList();
                }
                if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                {
                    nex = nex.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                }
                if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                {
                    nex = nex.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                }

                nex = nex.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();

                PageState.PageNumberState();
                JsonData lastData1 = new JsonData(nex, PageState.StartPage, PageState.EndPage);
                return Ok(lastData1);
            }

            if (previous == "previous") {
                
                if (1 == PageState.CurrentPage)
                {
                    List<Employee> prev1 = db.employee_tb.ToList();

                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;*/
                    int count1 = prev1.Count;
                    if (gender != null && gender.ToLower() == "male")
                    {
                        prev1 = prev1.Where(li => li.Gender == "Male").ToList();
                    }
                    else if (gender != null && gender.ToLower() == "female")
                    {
                        prev1 = prev1.Where(li => li.Gender == "Female").ToList();
                    }
                    if (status != null && status.ToLower() == "active")
                    {
                        prev1 = prev1.Where(li => li.Status == true).ToList();
                    }
                    else if (status != null && status.ToLower() == "inactive")
                    {
                        prev1 = prev1.Where(li => li.Status == false).ToList();
                    }
                    if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        prev1 = prev1.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                    }
                    if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        prev1 = prev1.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                    }
                    prev1 = prev1.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    
                    PageState.PageNumberState();
                    JsonData previousData1 = new JsonData(prev1, PageState.StartPage, PageState.EndPage);
                    return Ok(previousData1);
                }
                PageState.CurrentPage = PageState.CurrentPage - 1;
                
                /*ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;*/
                PageState.CurrentItem -= PageState.ItemPerPage;
                List<Employee> prev = db.employee_tb.ToList();
                
                if (gender != null && gender.ToLower() == "male")
                {
                    prev = prev.Where(li => li.Gender == "Male").ToList();
                }
                else if (gender != null && gender.ToLower() == "female")
                {
                    prev = prev.Where(li => li.Gender == "Female").ToList();
                }
                if (status != null && status.ToLower() == "active")
                {
                    prev = prev.Where(li => li.Status == true).ToList();
                }
                else if (status != null && status.ToLower() == "inactive")
                {
                    prev = prev.Where(li => li.Status == false).ToList();
                }
                if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                {
                    prev = prev.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                }
                if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                {
                    prev = prev.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                }
                prev = prev.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();

                
                PageState.PageNumberState();
                JsonData previousData = new JsonData(prev, PageState.StartPage, PageState.EndPage);
                return Ok(previousData);
            }

            if (page > 0) {
                if (PageState.CurrentPage == page) {
                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;*/
                    List<Employee> same = db.employee_tb.ToList();
                    
                    if (gender != null && gender.ToLower() == "male")
                    {
                        same = same.Where(li => li.Gender == "Male").ToList();
                    }
                    else if (gender != null && gender.ToLower() == "female")
                    {
                        same = same.Where(li => li.Gender == "Female").ToList();
                    }
                    if (status != null && status.ToLower() == "active")
                    {
                        same = same.Where(li => li.Status == true).ToList();
                    }
                    else if (status != null && status.ToLower() == "inactive")
                    {
                        same = same.Where(li => li.Status == false).ToList();
                    }
                    if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        same = same.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                    }
                    if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                    {
                        same = same.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                    }

                    same =same.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();

                    
                    JsonData sameData = new JsonData(same, PageState.StartPage, PageState.EndPage);
                    return Ok(sameData);
                }
                PageState.CurrentPage = page;
                int skip = (PageState.CurrentPage-1)*PageState.ItemPerPage;
                PageState.CurrentItem =skip;
                List<Employee> defined = db.employee_tb.ToList();
                
                /*ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;*/
                if (gender != null && gender.ToLower() == "male")
                {
                    defined = defined.Where(li => li.Gender == "Male").ToList();
                }
                else if (gender != null && gender.ToLower() == "female")
                {
                    defined = defined.Where(li => li.Gender == "Female").ToList();
                }
                if (status != null && status.ToLower() == "active")
                {
                    defined = defined.Where(li => li.Status == true).ToList();
                }
                else if (status != null && status.ToLower() == "inactive")
                {
                    defined = defined.Where(li => li.Status == false).ToList();
                }
                if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
                {
                    defined = defined.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
                }
                if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00"))
                {
                    defined = defined.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
                }

                defined = defined.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();

                
                PageState.PageNumberState();
                JsonData definedData = new JsonData(defined, PageState.StartPage, PageState.EndPage);
                return Ok(definedData);
            }
            
            List<Employee> li = db.employee_tb.ToList();
            

            if (gender != null && gender.ToLower() == "male")
            {
                li = li.Where(li => li.Gender == "Male").ToList();

            } else if (gender != null && gender.ToLower() == "female") {
                li =li.Where(li => li.Gender == "Female").ToList();
            }
            if (status != null && status.ToLower() == "active")
            {
                li = li.Where(li => li.Status == true).ToList();
            }
            else if (status != null && status.ToLower() == "inactive")
            {
                li = li.Where(li => li.Status == false).ToList();
            }
            
            if (filterCreated == "created" && createdFromDate != DateTime.Parse("01-01-0001 00:00:00") && createdToDate != DateTime.Parse("01-01-0001 00:00:00"))
            {
                li = li.Where(li => li.created_date >= createdFromDate && li.created_date <= createdToDate).ToList();
            }
            if (filterModified == "modified" && modifiedFromDate != DateTime.Parse("01-01-0001 00:00:00") && modifiedToDate != DateTime.Parse("01-01-0001 00:00:00")) {
                li = li.Where(li => li.modifed_date >= modifiedFromDate && li.modifed_date <= modifiedToDate).ToList();
            }
            PageState.CurrentPage = 1;
            PageState.ItemPerPage = itemPerPage != 0 ? itemPerPage : PageState.ItemPerPage;
            PageState.CurrentItem = 0;
            PageState.TotalItems = li.Count;
            PageState.TotalPages = (int)Math.Ceiling((double)PageState.TotalItems / (double)PageState.ItemPerPage);
            PageState.PageNumberState();
            /* ViewData["startPage"] = PageState.StartPage;
             ViewData["endPage"] = PageState.EndPage;*/
            List<Employee> data = li.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
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


        private static string lastSearch;
        //Search Operation
        public IActionResult Search([FromQuery] string next, [FromQuery] string previous, [FromQuery] int page, [FromForm] int itemPerPage, [FromQuery] string search, [FromQuery] string sort,[FromQuery] string gender, [FromQuery] string status,[FromQuery] string filterDate ,[FromQuery] DateTime toDate, [FromQuery] DateTime fromDate)
        {
            if (sort != null)
            {
                if (state == 2)
                {
                    state = 0;
                }
                else
                {
                    state++;
                }
            }
            if (state == 1)
            {
                if (next == "next")
                {
                    if (PageState.TotalPages == PageState.CurrentPage)
                    {
                        List<Employee> last = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                        PageState.PageNumberState();
                        JsonData lastData = new JsonData(last, PageState.StartPage, PageState.EndPage);
                        return Ok(lastData);

                    }
                    PageState.CurrentPage = PageState.CurrentPage + 1;
                    PageState.PageNumberState();
                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage; */
                    PageState.CurrentItem += PageState.ItemPerPage;
                    List<Employee> nex = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    JsonData lastData1 = new JsonData(nex, PageState.StartPage, PageState.EndPage);
                    return Ok(lastData1);
                }

                if (previous == "previous")
                {

                    if (1 == PageState.CurrentPage)
                    {
                        List<Employee> prev1 = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                        PageState.PageNumberState();
                        /*ViewData["startPage"] = PageState.StartPage;
                        ViewData["endPage"] = PageState.EndPage; */
                        JsonData previousData1 = new JsonData(prev1, PageState.StartPage, PageState.EndPage);
                        return Ok(previousData1);
                    }
                    PageState.CurrentPage = PageState.CurrentPage - 1;
                    PageState.PageNumberState();
                    /* ViewData["startPage"] = PageState.StartPage;
                     ViewData["endPage"] = PageState.EndPage; */
                    PageState.CurrentItem -= PageState.ItemPerPage;
                    List<Employee> prev = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    JsonData previousData = new JsonData(prev, PageState.StartPage, PageState.EndPage);
                    return Ok(previousData);
                }

                if (page > 0)
                {
                    if (PageState.CurrentPage == page)
                    {
                        /*ViewData["startPage"] = PageState.StartPage;
                        ViewData["endPage"] = PageState.EndPage; */
                        List<Employee> same = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                        JsonData sameData = new JsonData(same, PageState.StartPage, PageState.EndPage);
                        return Ok(sameData);
                    }
                    PageState.CurrentPage = page;
                    int skip = (PageState.CurrentPage - 1) * PageState.ItemPerPage;
                    PageState.CurrentItem = skip;
                    List<Employee> defined = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).OrderBy(elem => elem.Name).Skip(skip).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage; */
                    JsonData definedData = new JsonData(defined, PageState.StartPage, PageState.EndPage);
                    return Ok(definedData);
                }

                lastSearch = search == null ? lastSearch : search;

                List<Employee> searchedData1 = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).OrderBy(elem => elem.Name).ToList();
                PageState.CurrentPage = 1;
                //PageState.ItemPerPage = itemPerPage == 0 ? 2 : itemPerPage;
                PageState.ItemPerPage = itemPerPage != 0 ? itemPerPage : PageState.ItemPerPage;
                PageState.CurrentItem = 0;
                PageState.TotalItems = searchedData1.Count;
                PageState.TotalPages = (int)Math.Ceiling((double)PageState.TotalItems / (double)PageState.ItemPerPage);
                PageState.PageNumberState();
                /*ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage; */
                JsonData frontData1 = new JsonData(searchedData1.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList(), PageState.StartPage, PageState.EndPage);
                return Ok(frontData1);
            }

            else if (state == 2)
            {
                if (next == "next")
                {
                    if (PageState.TotalPages == PageState.CurrentPage)
                    {
                        List<Employee> last = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                        PageState.PageNumberState();
                        /*ViewData["startPage"] = PageState.StartPage;
                        ViewData["endPage"] = PageState.EndPage; */
                        JsonData lastData = new JsonData(last, PageState.StartPage, PageState.EndPage);
                        return Ok(lastData);

                    }
                    PageState.CurrentPage = PageState.CurrentPage + 1;
                    PageState.PageNumberState();
                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage; */
                    PageState.CurrentItem += PageState.ItemPerPage;
                    List<Employee> nex = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    JsonData lastData1 = new JsonData(nex, PageState.StartPage, PageState.EndPage);
                    return Ok(lastData1);
                }

                if (previous == "previous")
                {

                    if (1 == PageState.CurrentPage)
                    {
                        List<Employee> prev1 = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                        PageState.PageNumberState();
                        /*ViewData["startPage"] = PageState.StartPage;
                        ViewData["endPage"] = PageState.EndPage; */
                        JsonData previousData1 = new JsonData(prev1, PageState.StartPage, PageState.EndPage);
                        return Ok(previousData1);
                    }
                    PageState.CurrentPage = PageState.CurrentPage - 1;
                    PageState.PageNumberState();
                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage; */
                    PageState.CurrentItem -= PageState.ItemPerPage;
                    List<Employee> prev = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    JsonData previousData = new JsonData(prev, PageState.StartPage, PageState.EndPage);
                    return Ok(previousData);
                }

                if (page > 0)
                {
                    if (PageState.CurrentPage == page)
                    {
                        /*ViewData["startPage"] = PageState.StartPage;
                        ViewData["endPage"] = PageState.EndPage; */
                        List<Employee> same = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                        JsonData sameData = new JsonData(same, PageState.StartPage, PageState.EndPage);
                        return Ok(sameData);
                    }
                    PageState.CurrentPage = page;
                    int skip = (PageState.CurrentPage - 1) * PageState.ItemPerPage;
                    PageState.CurrentItem = skip;
                    List<Employee> defined = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).OrderByDescending(elem => elem.Name).Skip(skip).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage; */
                    JsonData definedData = new JsonData(defined, PageState.StartPage, PageState.EndPage);
                    return Ok(definedData);
                }

                lastSearch = search == null ? lastSearch : search;

                List<Employee> searchedData1 = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).OrderByDescending(elem => elem.Name).ToList();
                PageState.CurrentPage = 1;
                //PageState.ItemPerPage = itemPerPage == 0 ? 2 : itemPerPage;
                PageState.ItemPerPage = itemPerPage != 0 ? itemPerPage : PageState.ItemPerPage;
                PageState.CurrentItem = 0;
                PageState.TotalItems = searchedData1.Count;
                PageState.TotalPages = (int)Math.Ceiling((double)PageState.TotalItems / (double)PageState.ItemPerPage);
                PageState.PageNumberState();
                /*ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage; */
                JsonData frontData1 = new JsonData(searchedData1.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList(), PageState.StartPage, PageState.EndPage);
                return Ok(frontData1);
            }




            if (next == "next")
            {
                if (PageState.TotalPages == PageState.CurrentPage)
                {
                    List<Employee> last = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;*/
                    JsonData lastData = new JsonData(last, PageState.StartPage, PageState.EndPage);
                    return Ok(lastData);
                    
                }
                PageState.CurrentPage = PageState.CurrentPage + 1;
                PageState.PageNumberState();
                /*ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;*/
                PageState.CurrentItem += PageState.ItemPerPage;
                List<Employee> nex = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                JsonData lastData1 = new JsonData(nex, PageState.StartPage, PageState.EndPage);
                return Ok(lastData1);
            }

            if (previous == "previous")
            {

                if (1 == PageState.CurrentPage)
                {
                    List<Employee> prev1 = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;*/
                    JsonData previousData1 = new JsonData(prev1, PageState.StartPage, PageState.EndPage);
                    return Ok(previousData1);
                }
                PageState.CurrentPage = PageState.CurrentPage - 1;
                PageState.PageNumberState();
               /* ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;*/
                PageState.CurrentItem -= PageState.ItemPerPage;
                List<Employee> prev = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                JsonData previousData = new JsonData(prev, PageState.StartPage, PageState.EndPage);
                return Ok(previousData);
            }

            if (page > 0)
            {
                if (PageState.CurrentPage == page)
                {
                    /*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;*/
                    List<Employee> same = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    JsonData sameData = new JsonData(same, PageState.StartPage, PageState.EndPage);
                    return Ok(sameData);
                }
                PageState.CurrentPage = page;
                int skip = (PageState.CurrentPage - 1) * PageState.ItemPerPage;
                PageState.CurrentItem = skip;
                List<Employee> defined = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).Skip(skip).Take(PageState.ItemPerPage).ToList();
                PageState.PageNumberState();
                /*ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;*/
                JsonData definedData = new JsonData(defined, PageState.StartPage, PageState.EndPage);
                return Ok(definedData);
            }
            if (itemPerPage != 0) {
                List<Employee> lastSearchedData = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).ToList();
                PageState.CurrentPage = 1;
                PageState.ItemPerPage = itemPerPage;
                PageState.CurrentItem = 0;
                PageState.TotalItems = lastSearchedData.Count;
                PageState.TotalPages = (int)Math.Ceiling((double)PageState.TotalItems / (double)PageState.ItemPerPage);
                PageState.PageNumberState();
                /* ViewData["startPage"] = PageState.StartPage;
                 ViewData["endPage"] = PageState.EndPage;*/
                JsonData frontData1 = new JsonData(lastSearchedData.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList(), PageState.StartPage, PageState.EndPage);
                return Ok(frontData1);
            }
                
                lastSearch = search==null?lastSearch:search;
                List<Employee> searchedData = (db.employee_tb).Where(elem => elem.Name.Contains(lastSearch)).ToList();
                PageState.CurrentPage = 1;
                //PageState.ItemPerPage = itemPerPage==0?PageState.ItemPerPage:itemPerPage;
                PageState.CurrentItem = 0;
                PageState.TotalItems = searchedData.Count;
                PageState.TotalPages = (int)Math.Ceiling((double)PageState.TotalItems / (double)PageState.ItemPerPage);
                PageState.PageNumberState();
               /* ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;*/
                JsonData frontData = new JsonData(searchedData.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList(), PageState.StartPage, PageState.EndPage);
                return Ok(frontData);
           
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="previous"></param>
        /// <param name="page"></param>
        /// <param name="itemPerPage"></param>
        /// <returns></returns>



        
        /*public IActionResult FilterBy([FromQuery] string next, [FromQuery] string previous, [FromQuery] int page, [FromForm] int itemPerPage) {
             if (next == "next")
            {
                if (PageState.TotalPages == PageState.CurrentPage)
                {
                    List<Employee> last = (db.employee_tb).OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    JsonData lastData = new JsonData(last, PageState.StartPage, PageState.EndPage);
                    return Ok(lastData);
                    
                }
                PageState.CurrentPage = PageState.CurrentPage + 1;
                PageState.PageNumberState();
                *//*ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;*//*
                PageState.CurrentItem += PageState.ItemPerPage;
                List<Employee> nex = (db.employee_tb).OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                JsonData lastData1 = new JsonData(nex, PageState.StartPage, PageState.EndPage);
                return Ok(lastData1);
            }

            if (previous == "previous")
            {

                if (1 == PageState.CurrentPage)
                {
                    List<Employee> prev1 = (db.employee_tb).OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    *//*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;*//*
                    JsonData previousData1 = new JsonData(prev1, PageState.StartPage, PageState.EndPage);
                    return Ok(previousData1);
                }
                PageState.CurrentPage = PageState.CurrentPage - 1;
                PageState.PageNumberState();
               *//* ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;*//*
                PageState.CurrentItem -= PageState.ItemPerPage;
                List<Employee> prev = (db.employee_tb).OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                JsonData previousData = new JsonData(prev, PageState.StartPage, PageState.EndPage);
                return Ok(previousData);
            }

            if (page > 0)
            {
                if (PageState.CurrentPage == page)
                {
                    *//*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;*//*
                    List<Employee> same = (db.employee_tb).OrderBy(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    JsonData sameData = new JsonData(same, PageState.StartPage, PageState.EndPage);
                    return Ok(sameData);
                }
                PageState.CurrentPage = page;
                int skip = (PageState.CurrentPage - 1) * PageState.ItemPerPage;
                PageState.CurrentItem = skip;
                List<Employee> defined = (db.employee_tb).OrderBy(elem => elem.Name).Skip(skip).Take(PageState.ItemPerPage).ToList();
                PageState.PageNumberState();
                *//*ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;*//*
                JsonData definedData = new JsonData(defined, PageState.StartPage, PageState.EndPage);
                return Ok(definedData);
            }

                List<Employee> searchedData = (db.employee_tb).OrderBy(elem=>elem.Name).ToList();
                PageState.CurrentPage = 1;
                PageState.ItemPerPage = itemPerPage == 0 ? 2 : itemPerPage;
                PageState.CurrentItem = 0;
                PageState.TotalItems = searchedData.Count;
                PageState.TotalPages = (int)Math.Ceiling((double)PageState.TotalItems / (double)PageState.ItemPerPage);
                PageState.PageNumberState();
                *//*ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;*//*
                JsonData frontData = new JsonData(searchedData.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList(), PageState.StartPage, PageState.EndPage);
                return Ok(frontData);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="previous"></param>
        /// <param name="page"></param>
        /// <param name="itemPerPage"></param>
        /// <returns></returns>
        public IActionResult FilterByDescending([FromQuery] string next, [FromQuery] string previous, [FromQuery] int page, [FromForm] int itemPerPage)
        {
            if (next == "next")
            {
                if (PageState.TotalPages == PageState.CurrentPage)
                {
                    List<Employee> last = (db.employee_tb).OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    *//*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;*//*
                    JsonData lastData = new JsonData(last, PageState.StartPage, PageState.EndPage);
                    return Ok(lastData);
                    
                }
                PageState.CurrentPage = PageState.CurrentPage + 1;
                PageState.PageNumberState();
                *//*ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;*//*
                PageState.CurrentItem += PageState.ItemPerPage;
                List<Employee> nex = (db.employee_tb).OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                JsonData lastData1 = new JsonData(nex, PageState.StartPage, PageState.EndPage);
                return Ok(lastData1);
            }

            if (previous == "previous")
            {

                if (1 == PageState.CurrentPage)
                {
                    List<Employee> prev1 = (db.employee_tb).OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    PageState.PageNumberState();
                    *//*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;*//*
                    JsonData previousData1 = new JsonData(prev1, PageState.StartPage, PageState.EndPage);
                    return Ok(previousData1);
                }
                PageState.CurrentPage = PageState.CurrentPage - 1;
                PageState.PageNumberState();
                *//*ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;*//*
                PageState.CurrentItem -= PageState.ItemPerPage;
                List<Employee> prev = (db.employee_tb).OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                JsonData previousData = new JsonData(prev, PageState.StartPage, PageState.EndPage);
                return Ok(previousData);
            }

            if (page > 0)
            {
                if (PageState.CurrentPage == page)
                {
                    *//*ViewData["startPage"] = PageState.StartPage;
                    ViewData["endPage"] = PageState.EndPage;*//*
                    List<Employee> same = (db.employee_tb).OrderByDescending(elem => elem.Name).Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList();
                    JsonData sameData = new JsonData(same, PageState.StartPage, PageState.EndPage);
                    return Ok(sameData);
                }
                PageState.CurrentPage = page;
                int skip = (PageState.CurrentPage - 1) * PageState.ItemPerPage;
                PageState.CurrentItem = skip;
                List<Employee> defined = (db.employee_tb).OrderByDescending(elem => elem.Name).Skip(skip).Take(PageState.ItemPerPage).ToList();
                PageState.PageNumberState();
                *//*ViewData["startPage"] = PageState.StartPage;
                ViewData["endPage"] = PageState.EndPage;*//*
                JsonData definedData = new JsonData(defined, PageState.StartPage, PageState.EndPage);
                return Ok(definedData);
            }

            List<Employee> searchedData = (db.employee_tb).OrderByDescending(elem => elem.Name).ToList();
            PageState.CurrentPage = 1;
            PageState.ItemPerPage = itemPerPage == 0 ? 2 : itemPerPage;
            PageState.CurrentItem = 0;
            PageState.TotalItems = searchedData.Count;
            PageState.TotalPages = (int)Math.Ceiling((double)PageState.TotalItems / (double)PageState.ItemPerPage);
            PageState.PageNumberState();
            *//*ViewData["startPage"] = PageState.StartPage;
            ViewData["endPage"] = PageState.EndPage;*//*
            JsonData frontData = new JsonData(searchedData.Skip(PageState.CurrentItem).Take(PageState.ItemPerPage).ToList(), PageState.StartPage, PageState.EndPage);
            return Ok(frontData);
        }*/


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
