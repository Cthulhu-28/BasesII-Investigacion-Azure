using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections.ObjectModel;
using System.Transactions;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Common;
using cnWorld;
using System.Xml.Serialization;

/// <summary>
/// Summary description for Solicitud
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Solicitud : System.Web.Services.WebService
{
    static List<dbWorldDataContext> context = new List<dbWorldDataContext>();
    const String img_default = "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";
    public Solicitud()
    {
       
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    /// <summary>
    /// Finds a country with a given id
    /// </summary>
    /// <param name="id">Id of the country</param>
    /// <returns>Returns a references to the country</returns>
    [WebMethod]
    public clCountry FindCountry(decimal id,int? tran)
    {
        clCountry country = null;
        var db = getTransaction(getTranNumber(tran));
        var vCountry = from s in db.COUNTRies where s.COUNTRYID == id select s;
        if (vCountry.Count() > 0)
        {
            var p = vCountry.First();
            country = new clCountry
            {
                ANTHEM = null,
                COUNTRYID = p.COUNTRYID,
                FILEID = p.FILEID,
                COUNTRYNAME = p.COUNTRYNAME,
                POPULATION = p.POPULATION,
                PRESIDENT = p.PRESIDENT,
                FLAGB64 = Convert.ToBase64String(p.FLAG.ToArray()),
                FLAG = null,
                ANTHEMB64 = Convert.ToBase64String(p.ANTHEM.ToArray()),
                AREA = p.AREA
            };
        }
        return country;
    }
    [WebMethod]
    public ObservableCollection<clCountry> GetCountries(Object txn)
    {

        var list = new ObservableCollection<clCountry>();
        clCountry tmp;
        var db = new dbWorldDataContext();
        if (txn != null) db.Transaction = (DbTransaction)txn;
        var vCountry = from s in db.COUNTRies select s;
        foreach (var row in vCountry)
        {
            tmp = new clCountry(row.COUNTRYID, row.COUNTRYNAME, row.AREA, row.POPULATION, null, null, row.PRESIDENT);
            tmp.COUNTRYID = row.COUNTRYID;
            list.Add(tmp);
        }
        return list;

    }
    [WebMethod]
    public bool AddCountries(List<clCountry> countries)
    {
        //
        var db = new dbWorldDataContext();
        foreach (var row in countries)
        {
            enWorld.COUNTRY tmp = new enWorld.COUNTRY
            {
                AREA = row.AREA,
                FLAG = row.FLAG,
                ANTHEM = row.ANTHEM,
                COUNTRYNAME = row.COUNTRYNAME,
                POPULATION = row.POPULATION,
                PRESIDENT = row.PRESIDENT,
                FILEID = Guid.NewGuid()
            };
            db.COUNTRies.InsertOnSubmit(tmp);
        }
        db.SubmitChanges();
        return true;
    }
    /// <summary>
    /// Allows to add a country
    /// </summary>
    /// <param name="country">The country to be inserted</param>
    /// <returns>Return true if it was added successfully</returns>
    [WebMethod]
    public int AddCountry(clCountry country,int? tran)
    {

        int _tran = useTransaction(getTranNumber(tran));
        var db = getTransaction(_tran);
        var row = country;
        
        enWorld.COUNTRY tmp = new enWorld.COUNTRY
        {
            COUNTRYID=row.COUNTRYID,
            AREA = row.AREA,
            FLAG = row.FLAG,
            ANTHEM = row.ANTHEM,
            COUNTRYNAME = row.COUNTRYNAME,
            POPULATION = row.POPULATION,
            PRESIDENT = row.PRESIDENT,
            FILEID = Guid.NewGuid()
        };
        db.COUNTRies.InsertOnSubmit(tmp);
        db.SubmitChanges();
        return _tran;
    }
    [WebMethod]
    public DbTransaction test(DbTransaction txn)
    {
        var db = new dbWorldDataContext();

        return txn;
    }
    /// <summary>
    /// Returns a portion of the countries given a page number
    /// </summary>
    /// <param name="page">The page number</param>
    /// <returns>Returns a list of countries ordered alphabetically at a given page number </returns>
    [WebMethod]
    public ObservableCollection<clCountry> GetCountiesAt(decimal page,int? tran)
    {
        clCountry tmp = null;
        var list = new ObservableCollection<clCountry>();
        var db = getTransaction(getTranNumber(tran));
        
        var result = db.SELECT_PIECE(page);
        foreach (var row in result)
        {
            tmp = new clCountry
            {
                COUNTRYID = row.COUNTRYID,
                AREA = row.AREA,
                POPULATION = row.POPULATION,
                PRESIDENT = row.PRESIDENT,
                COUNTRYNAME = row.COUNTRYNAME,
                ANTHEM = null,
                FLAG = null,
                FLAGB64 = ""
            };
            list.Add(tmp);
        }
        return list;
    }
    /// <summary>
    /// Given a country update its atrributes
    /// </summary>
    /// <param name="country">The country to be updated</param>
    /// <returns>Returns true if it was updated successfully, false otherwise</returns>
    [WebMethod]
    public int UpdateCountry(clCountry country,decimal old_id,int? tran)
    {
        int _tran = useTransaction(getTranNumber(tran));
        var db = getTransaction(_tran);
        var record = (from s in db.COUNTRies where s.COUNTRYID == old_id select s).Single();
        if (country.FLAG != null)
            record.FLAG = country.FLAG;
        if (country.ANTHEM != null)
            record.ANTHEM = country.ANTHEM;
        record.AREA = country.AREA;
        record.COUNTRYNAME = country.COUNTRYNAME;
        record.PRESIDENT = country.PRESIDENT;
        record.POPULATION = country.POPULATION;
        record.COUNTRYID = country.COUNTRYID;
        db.SubmitChanges();
        return _tran;
    }
    [WebMethod]
    public int deleteCountry(decimal id,int? tran)
    {
        int _tran = useTransaction(getTranNumber(tran));
        var db = getTransaction(_tran);
        var record = (from s in db.COUNTRies where s.COUNTRYID == id select s).Single();
        db.COUNTRies.DeleteOnSubmit(record);
        db.SubmitChanges();
        return _tran;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [WebMethod]
    public clPerson FindPerson(decimal id,int? tran)
    {
        clPerson person = null;
        var db = getTransaction(getTranNumber(tran));
        var vPerson = from s in db.PERSONs where s.IDENTIFICATION == id select s;
        if (vPerson.Count() > 0)
        {
            var p = vPerson.First();
            person = new clPerson
            {
                BIRTH_COUNTRY = p.COUNTRY.COUNTRYNAME,
                BIRTH_COUNTRY_ID = p.BIRTH_COUNTRY,
                BIRTH_DATE = p.BIRTH_DATE,
                EMAIL=p.EMAIL,
                IDENTIFICATION=p.IDENTIFICATION,
                NAME=p.NAME,
                PHOTOB64= p.PHOTO==null? img_default:Convert.ToBase64String(p.PHOTO.ToArray()),
                RESIDENCE_COUNTRY=p.COUNTRY1.COUNTRYNAME,
                RESIDENCE_COUNTRY_ID=p.RESIDENCE_COUNTRY,
                VIDEO=p.VIDEO
            };
            if(p.PHOTO!=null)
            {
                person.PHOTOB64 = Convert.ToBase64String(p.PHOTO.ToArray());
            }
        }
        return person;
    }
    /// <summary>
    /// Allows to add a country
    /// </summary>
    /// <param name="country">The country to be inserted</param>
    /// <returns>Return true if it was added successfully</returns>
    [WebMethod]
    public int AddPerson(clPerson person,int? tran)
    {
        int _tran = useTransaction(getTranNumber(tran));
        var db = getTransaction(_tran);
        var row = person;

        enWorld.PERSON tmp = new enWorld.PERSON
        {
            BIRTH_COUNTRY = row.BIRTH_COUNTRY_ID,
            BIRTH_DATE = row.BIRTH_DATE,
            EMAIL = row.EMAIL,
            IDENTIFICATION = row.IDENTIFICATION,
            NAME = row.NAME,
            RESIDENCE_COUNTRY = row.RESIDENCE_COUNTRY_ID,
            VIDEO = row.VIDEO,
            FILEID = Guid.NewGuid(),
            PHOTO=person.PHOTO
        };
        db.PERSONs.InsertOnSubmit(tmp);
        db.SubmitChanges();
        return _tran;
    }
    /// <summary>
    /// Returns a portion of the countries given a page number
    /// </summary>
    /// <param name="page">The page number</param>
    /// <returns>Returns a list of countries ordered alphabetically at a given page number </returns>
    [WebMethod]
    public ObservableCollection<clPerson> GetPeopleAt(decimal page,decimal page_size,int? tran)
    {
        clPerson tmp = null;
        var list = new ObservableCollection<clPerson>();
        var db = getTransaction(getTranNumber(tran));

        var result = db.SELECT_PIECE_PERSON(page,page_size);
        foreach (var row in result)
        {
            tmp = new clPerson
            {
                BIRTH_COUNTRY=row.BNAME,
                BIRTH_COUNTRY_ID=row.BIRTH_COUNTRY,
                BIRTH_DATE=row.BIRTH_DATE,
                EMAIL=row.EMAIL,
                IDENTIFICATION=row.IDENTIFICATION,
                NAME=row.NAME,
                RESIDENCE_COUNTRY=row.RNAME,
                RESIDENCE_COUNTRY_ID=row.RESIDENCE_COUNTRY
            };
            list.Add(tmp);
        }
        return list;
    }
    /// <summary>
    /// Given a country update its atrributes
    /// </summary>
    /// <param name="country">The country to be updated</param>
    /// <returns>Returns true if it was updated successfully, false otherwise</returns>
    [WebMethod]
    public int UpdatePerson(clPerson person,decimal old_id,int? tran)
    {
        int _tran = useTransaction(getTranNumber(tran));
        var db = getTransaction(_tran);
        var record = (from s in db.PERSONs where s.IDENTIFICATION == old_id select s).Single();
        db.PERSONs.DeleteOnSubmit(record);
        db.SubmitChanges();
        if (person.PHOTO != null)
            record.PHOTO = person.PHOTO;
        enWorld.PERSON p = new enWorld.PERSON();
        p.BIRTH_COUNTRY= person.BIRTH_COUNTRY_ID;
        p.BIRTH_DATE=person.BIRTH_DATE;
        p.EMAIL= person.EMAIL;
        p.NAME= person.NAME;
        p.RESIDENCE_COUNTRY = person.RESIDENCE_COUNTRY_ID;
        p.VIDEO = person.VIDEO;
        p.IDENTIFICATION = person.IDENTIFICATION;
        db.PERSONs.InsertOnSubmit(p);
        db.SubmitChanges();
        return _tran;
    }
    [WebMethod]
    public int deletePerson(decimal id,int? tran)
    {
        var _tran = useTransaction(getTranNumber(tran));
        var db = getTransaction(_tran);
        var record = (from s in db.PERSONs where s.IDENTIFICATION == id select s).Single();
        db.PERSONs.DeleteOnSubmit(record);
        db.SubmitChanges();
        return _tran;
    }
    [WebMethod]
    public ObservableCollection<clCountry> GetPaisesLista(int? tran)
    {
        var db = getTransaction(getTranNumber(tran));
        var list = (from c in db.COUNTRies
                    select new clCountry
                    {
                        COUNTRYID = c.COUNTRYID,
                        COUNTRYNAME = c.COUNTRYNAME
                    }).ToList();
        var _list = new ObservableCollection<clCountry>(list);
        return _list;
    }
    /// <summary>
    /// Gets the current transaction. If there's no transaction it creates it
    /// </summary>
    /// <returns>The transaction</returns>
    private dbWorldDataContext getTransaction(int tran)
    {
        return tran==-1||context.ElementAt(tran) == null ? new dbWorldDataContext() : context.ElementAt(tran);
    }
    /// <summary>
    /// Gets the current connection. If the transaction's connection is null creates a new transaction
    /// </summary>
    /// <returns>The transaction</returns>
    private int useTransaction(int tran)
    {
        if(tran==-1||context.ElementAt(tran) ==null)
        {
            var _context = new dbWorldDataContext();
            _context.Connection.Open();
            var txn = _context.Connection.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
            _context.Transaction = txn;
            context.Add(_context);
            return context.Count - 1;
        }
       
        return tran;
    }
    private int getTranNumber(int? tran)
    {
        return tran == null ? -1 : (int)tran;
    }
    /// <summary>
    /// Rollback the current transaction
    /// </summary>
    [WebMethod]
    public void RollBack(int? tran)
    {
        var db = getTransaction(getTranNumber(tran));
        if(db!=null && db.Transaction!=null)
        {
            db.Transaction.Rollback();
            db.Transaction = null;
            db.Connection.Close();
            db = null;
        }
    }
    /// <summary>
    /// Commits the current transaction
    /// </summary>
    [WebMethod]
    public void Commit(int? tran)
    {
        var db = getTransaction(getTranNumber(tran));
        if(db!=null && db.Transaction!=null)
        {
            db.Transaction.Commit();
            db.Transaction = null;
            db.Connection.Close();
            db = null;
        }
    }
    [WebMethod]
    public ObservableCollection<clCountry> GET_COUNTRIES_INFO(Int64 page, Int64 size)
    {

        var db = new dbWorldDataContext();
        ObservableCollection<clCountry> list = new ObservableCollection<clCountry>();
        foreach(var row in db.COUNTRY_INFO(page,size))
        {
            list.Add(new clCountry
            {
                AREA = row.AGE == null ? -1 : (decimal)row.AGE,
                POPULATION = row.POPULATION == null ? -1 : (decimal)row.POPULATION,
                COUNTRYNAME=row.COUNTRYNAME,
                COUNTRYID=row.COUNTRYID
            });
        }
        return list;
    }
    [WebMethod]
    public ObservableCollection<clCountry> PEOPLE_YEAR_COUNTRY(decimal id,Int64 page, Int64 size)
    {

        var db = new dbWorldDataContext();
        ObservableCollection<clCountry> list = new ObservableCollection<clCountry>();
        if (page <= 0) return list;
        foreach (var row in db.PEOPLE_YEAR_COUNTRY(id,page, size))
        {
            list.Add(new clCountry
            {
                POPULATION = row.BORN == null ? -1 : (decimal)row.BORN,
                AREA = row.BORN == null ? -1 :(decimal)row.YEAR
            });
        }
        return list;
    }
    [WebMethod]
    public ObservableCollection<clCountry> PEOPLE_YEAR_ALLCOUNTRIES(Int64 page, Int64 size)
    {

        var db = new dbWorldDataContext();
        ObservableCollection<clCountry> list = new ObservableCollection<clCountry>();
        if (page <= 0) return list;
        foreach (var row in db.PEOPLE_YEAR_ALL_COUNTRIES(page, size))
        {
            list.Add(new clCountry
            {
                POPULATION = row.BORN == null ? -1 : (decimal)row.BORN,
                AREA = row.BORN == null ? -1 : (decimal)row.YEAR
            });
        }
        return list;
    }
    [WebMethod]
    public int? get_page_country(int page,int? tran)
    {
        var db = getTransaction(getTranNumber(tran));
        return (int?)db.GET_PAGE_COUNTRY(page).First().N;
    }
    [WebMethod]
    public int? get_page_person(int page, int? tran)
    {
        var db = getTransaction(getTranNumber(tran));
        return (int?)db.GET_PAGE_PERSON(page).First().N;
    }
    [WebMethod]
    public Boolean Generate_People(Decimal countries,Decimal population)
    {
        var db = new dbWorldDataContext();
        db.GENERATE_PEOPLE_fast(countries, population);
        return true;
    }
    [WebMethod]
    public ResultList query_II()
    {
        var db = new dbWorldDataContext();
        var list = new ResultList();
        foreach (var row in db.QUERY_II())
        {
            list.list.Add(new Result
            {
                count=row.COUNT,
                countryid=row.COUNTRYID,
                countryname=row.COUNTRYNAME,
                year=row.YEAR
            });
        }
        var d = db.MAX_DATES().ToList();
        if(d.Count()>0)
        { 
        list.max = d.ElementAt(0).maxi;
        list.min = d.ElementAt(0).mini;
        }
        return list;
        
    }
}
