using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingPagination.Models
{
    public static class PageState
    {
        public static int CurrentItem { get; set; }
        public static int CurrentPage { get; set; }

        public static int TotalPages { get; set; }
        public static int ItemPerPage { get; set; } = 2;

        public static int TotalItems { get; set; }

        public static int StartPage { get; set; }
        public static int EndPage { get; set; }

        public static void PageNumberState() {

            StartPage = CurrentPage;

            if (StartPage - 1 < 1)
            {
                StartPage = CurrentPage;
            }
            else {
                StartPage -= 1;
            }


            EndPage = 2;
            int endPage = EndPage + StartPage;
            if (endPage > TotalPages)
            {
                EndPage = TotalPages;
            }
            else
            {
                EndPage = endPage;
            }
        }
    }
}
