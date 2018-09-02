using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Result
/// </summary>
///

namespace TestFS.Models
{ 
    public class Result
    {
	
	    public int? count { get; set; }
        public String countryname { get; set; }
        public decimal? countryid { get; set; }

        public int? year { get; set; }
    
        public Result()
        {

        }

    }
}