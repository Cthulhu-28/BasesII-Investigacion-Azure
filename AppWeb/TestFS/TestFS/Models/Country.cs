using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestFS.Models
{
    public class Country
    {
        public Decimal COUNTRYID { get; set; }

        public String COUNTRYNAME { get; set; }

        public Decimal AREA { get; set; }

        public Decimal POPULATION { get; set; }

        public byte[] FLAG { get; set; }

        public byte[] ANTHEM { get; set; }

        public Decimal? PRESIDENT { get; set; }

        public String FLAGB64 { get; set; }

        public String ANTHEMB64 { get; set; }
    }
}