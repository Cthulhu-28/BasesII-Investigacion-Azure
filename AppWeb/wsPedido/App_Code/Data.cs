using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
/// <summary>
/// Summary description for Data
/// </summary>
public class Data
{
    public Data()
    {
        
    }
    public static DbTransaction txn { get; set; }
}