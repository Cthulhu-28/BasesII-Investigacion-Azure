using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for clPerson
/// </summary>
[DataContract]
public class clPerson
{
    [DataMember]
    public decimal IDENTIFICATION { get; set; }
    [DataMember]
    public String NAME { get; set; }
    [DataMember]
    public String BIRTH_COUNTRY { get; set; }
    [DataMember]
    public String RESIDENCE_COUNTRY { get; set; }
    [DataMember]
    public decimal BIRTH_COUNTRY_ID { get; set; }
    [DataMember]
    public decimal RESIDENCE_COUNTRY_ID { get; set; }
    [DataMember]
    public DateTime BIRTH_DATE { get; set; }
    [DataMember]
    public String EMAIL { get; set; }
    [DataMember]
    public byte[] PHOTO { get; set; }
    [DataMember]
    public String PHOTOB64 { get; set; }
    [DataMember]
    public String VIDEO { get; set; }

    public clPerson()
    {
       
    }

    public clPerson(decimal iDENTIFICATION, string nAME, string bIRTH_COUNTRY, string rESIDENCE_COUNTRY, decimal bIRTH_COUNTRY_ID, decimal rESIDENCE_COUNTRY_ID, DateTime bIRTH_DATE, string eMAIL, byte[] pHOTO, string vIDEO)
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