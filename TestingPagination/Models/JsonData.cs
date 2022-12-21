using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingPagination.Models
{
    public class JsonData
    {
        public JsonData(List<Employee> listData, int startPage,int endPage) {
            this.ListOfData = listData;
            this.StartPage = startPage;
            this.EndPage = endPage;
        }
        public List<Employee> ListOfData { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
