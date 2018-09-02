using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.ObjectModel;
/// <summary>
/// Summary description for clPais
/// </summary>
[DataContract]
public class clCountry
{
    public clCountry()
    {

    }
    [DataMember]
    public Decimal COUNTRYID { get; set; }
    [DataMember]
    public String COUNTRYNAME { get; set; }
    [DataMember]
    public Decimal AREA { get; set; }
    [DataMember]
    public Decimal POPULATION { get; set; }
    [DataMember]
    public Guid FILEID { get; set; }
    [DataMember]
    public byte[] FLAG { get; set; }
    [DataMember]
    public byte[] ANTHEM { get; set; }
    [DataMember]
    public Decimal? PRESIDENT { get; set; }
    [DataMember]
    public String FLAGB64 { get; set; }
    public String ANTHEMB64 { get; set; }
    public clCountry(decimal countryid, string countryname, decimal area, decimal population, byte[] flag, byte[] anthem, decimal? president)
    {
        this.COUNTRYID  = countryid;
        this.COUNTRYNAME = countryname;
        this.AREA = area;
        this.POPULATION = population;
        this.FLAG = flag;
        this.ANTHEM = anthem;
        this.PRESIDENT = president;
        //this.ID_file = new Guid();
    }
}