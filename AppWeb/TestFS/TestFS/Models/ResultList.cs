using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ResultList
/// </summary>
namespace TestFS.Models
{ 
    public class ResultList
    {
	
	    public List<Result> list { get; set; }
        public int? max { get; set; }
        public int? min { get; set; }

        public ResultList()
        {
            list = new List<Result>();
        }
	    public List<Result> getById(decimal? id)
        {
            return (from l in list where l.countryid == id select l).ToList();
        }
        public void delete(decimal? id)
        {
            list.RemoveAll(v => v.countryid == id);
        }
    }
}