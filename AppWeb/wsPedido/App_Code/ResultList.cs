using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ResultList
/// </summary>
public class ResultList
{
	
	public List<Result> list { get; set; }
    public int? max { get; set; }
    public int? min { get; set; }

    public ResultList()
    {
        list = new List<Result>();
    }
	
}