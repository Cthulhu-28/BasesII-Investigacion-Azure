using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestFS.Models
{
    public class clQuery
    {
        public List<Country> people_year { get; set; }
        public List<Country> people_year_all { get; set; }
        public Int64 page_1 { get; set; }
        public Int64 page_2 { get; set; }
       public clQuery()
        {
            people_year = new List<Country>();
            people_year_all = new List<Country>();
            page_1 = 1;
            page_2 = 1;
        }
    }
}