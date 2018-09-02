using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for clPerson
/// </summary>
namespace TestFS.Models
{ 
    public class Person
    {
    
        public decimal IDENTIFICATION { get; set; }
    
        public String NAME { get; set; }
    
        public String BIRTH_COUNTRY { get; set; }
    
        public String RESIDENCE_COUNTRY { get; set; }
    
        public decimal BIRTH_COUNTRY_ID { get; set; }
    
        public decimal RESIDENCE_COUNTRY_ID { get; set; }
    
        public DateTime BIRTH_DATE { get; set; }
    
        public String EMAIL { get; set; }
    
        public byte[] PHOTO { get; set; }
    
        public String PHOTOB64 { get; set; }
    
        public String VIDEO { get; set; }
        public String DATE { get; set; }
        public Person()
        {

        }

        public Person(decimal iDENTIFICATION, string nAME, string bIRTH_COUNTRY, string rESIDENCE_COUNTRY, decimal bIRTH_COUNTRY_ID, decimal rESIDENCE_COUNTRY_ID, DateTime bIRTH_DATE, string eMAIL, byte[] pHOTO, string vIDEO)
        {
            IDENTIFICATION = iDENTIFICATION;
            NAME = nAME;
            BIRTH_COUNTRY = bIRTH_COUNTRY;
            RESIDENCE_COUNTRY = rESIDENCE_COUNTRY;
            BIRTH_COUNTRY_ID = bIRTH_COUNTRY_ID;
            RESIDENCE_COUNTRY_ID = rESIDENCE_COUNTRY_ID;
            BIRTH_DATE = bIRTH_DATE;
            EMAIL = eMAIL;
            PHOTO = pHOTO;
            VIDEO = vIDEO;
        }
    }
}