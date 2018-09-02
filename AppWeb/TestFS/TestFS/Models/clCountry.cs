using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestFS.Models
{
    public class clCountry
    {
        public List<Country> loaded { get; set; }
        public List<Country> added { get; set; }
        public List<Person> people_loaded { get; set; }
        public Decimal page { get; set; }
        public Decimal p_page { get; set; }
        public clCountry()
        {
            p_page = 1;
            page = 1;
            loaded = new List<Country>();
            added = new List<Country>();
            people_loaded = new List<Person>();
        }
    }
}