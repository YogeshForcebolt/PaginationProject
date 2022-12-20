using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingPagination.Models
{
    public static class PageState
    {
        public static int CurrentPage { get; set; }

        public static int TotalPages { get; set; }
        public static int ItemPerPage { get; set; }

        public static int TotalItems { get; set; }
    }
}
