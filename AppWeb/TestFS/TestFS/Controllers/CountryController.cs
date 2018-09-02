using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Common;
using TestFS.Models;
namespace TestFS.Controllers
{
    public class CountryController : Controller
    {
        static int? n_c, n_p;
        ServiceReference1.SolicitudSoapClient ws = new ServiceReference1.SolicitudSoapClient();
        clCountry model = new clCountry();
        public void load(Decimal page)
        {
            clCountry model = new clCountry();
            List<Country> lista = new List<Country>();
            var t = getTransactionNumber();
            n_c = ws.get_page_country((int)10, t);
            page = n_c == null ? 1 : page >= (int)(n_c)||page==-1 ? (int)(n_c) : page;
            model.page = page;
           
            foreach (var row in ws.GetCountiesAt(page,t))
            {
                Country tmp = new Country();
                tmp.AREA = row.AREA;
                tmp.FLAG = row.FLAG;
                tmp.ANTHEM = row.ANTHEM;
                tmp.COUNTRYID = row.COUNTRYID;
                tmp.COUNTRYNAME = row.COUNTRYNAME;
                tmp.POPULATION = row.POPULATION;
                tmp.PRESIDENT = row.PRESIDENT;
                model.loaded.Add(tmp);
            }
            if (this.model != null) { this.model.loaded = model.loaded;model.page = page; }
            else { this.model = model; }
            setTransactionNumber(t);
        }
        public void loadPersons(Decimal page)
        {
            clCountry model = new clCountry();
            List<Person> list = new List<Person>();
            var t = getTransactionNumber();
            n_p = ws.get_page_person((int)10, t);
            page = n_p == null ? 1 : page>=(int)(n_p)||page==-1?(int)(n_p):page;
            foreach (var row in ws.GetPeopleAt(page,10,t))
            {
                Person tmp = new Person
                {
                    NAME=row.NAME,
                    BIRTH_COUNTRY=row.BIRTH_COUNTRY,
                    RESIDENCE_COUNTRY=row.RESIDENCE_COUNTRY,
                    BIRTH_DATE=row.BIRTH_DATE,
                    IDENTIFICATION=row.IDENTIFICATION
                };
                list.Add(tmp);
            }
            model.people_loaded = list;
            if (this.model != null) { this.model.people_loaded = model.people_loaded; this.model.p_page = page; }
            else { this.model = model; }
            setTransactionNumber(t);
        }
        public void loadLists()
        {
            var t = getTransactionNumber();
            ViewBag.birth_list = new SelectList(ws.GetPaisesLista(t), "COUNTRYID", "COUNTRYNAME");
            ViewBag.residence_list = new SelectList(ws.GetPaisesLista(t), "COUNTRYID", "COUNTRYNAME");
            setTransactionNumber(t);
        }
        public ActionResult Navigate(decimal page=1)
        {
            
            load(page);
          
            if(Request.IsAjaxRequest())
            {
                return PartialView("_PaisList", model.loaded);
            }
            
            return View(model);
        }
        public ActionResult NavigatePerson(decimal page=1)
        {
            loadPersons(page);
            if(Request.IsAjaxRequest())
            {
                return PartialView("_PeopleList", model.people_loaded);
            }
            return View(model);
        }

        public ActionResult Index(Decimal page=1,Decimal p_page=1)
        {
            load(page);
            loadPersons(p_page);
            loadLists();
            return View(model);
        }
        public ActionResult Test()
        {
            var t = getTransactionNumber();
            setTransactionNumber(t);
            return View();
        }
        [HttpPost]
        public ActionResult Test(String campo)
        {
            var t = getTransactionNumber();
            setTransactionNumber(t);
            Object obj = TempData["qcyo"];
            
            Console.WriteLine(campo);
            return View();
        }
        [HttpPost]

        public JsonResult Upload(HttpPostedFileBase uploadedFile)
        {
            int h = 0;
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InsertCountry(decimal countryid,decimal area, String country, decimal population, decimal? president, HttpPostedFileBase anthem_file, HttpPostedFileBase flag_file)
        {
            var t = getTransactionNumber();
            int txn;
            byte[] anthem = null, flag = null;

            if (anthem_file != null)
            {
                MemoryStream target = new MemoryStream();
                anthem_file.InputStream.CopyTo(target);
                anthem = target.ToArray();

            }
            if (flag_file != null)
            {
                MemoryStream target = new MemoryStream();
                flag_file.InputStream.CopyTo(target);
                flag = target.ToArray();
            }
            //Dictionary<d`cimal, byte[]> anthems = (Dictionary<decimal, byte[]>)TempData["audio"];
            //Dictionary<decimal, byte[]> flags = (Dictionary<decimal, byte[]>)TempData["img"];
            ServiceReference1.clCountry clCountry = new ServiceReference1.clCountry();
            clCountry.COUNTRYID = countryid;
            clCountry.AREA = area;
            clCountry.COUNTRYNAME = country;
            clCountry.POPULATION = population;
            clCountry.PRESIDENT = president;
            //clCountry.PRESIDENT = president==-1 ? null:president;
            clCountry.FLAG = flag;
            clCountry.ANTHEM = anthem;
            ServiceReference1.DbTransaction g;
            txn = ws.AddCountry(clCountry,t);
            setTransactionNumber(txn);
            var c = new Country();
            c.AREA = area;
            c.COUNTRYNAME = country;
            c.POPULATION = population;
            
            c.COUNTRYID = -1;
            load(1);
            loadPersons(1);
            loadLists();
            model.added.Add(c);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PaisList", model.added);
            }
            return View("Index",model);


        }
        public ActionResult rollback()
        {
            var t = getTransactionNumber();
            ws.RollBack(t);
            setTransactionNumber(null);
            load(1);
            loadPersons(1);
            loadLists();
            return View("Index", model);
        }
       
        public ActionResult commit()
        {
            var t = getTransactionNumber();
            ws.Commit(t);
            setTransactionNumber(null);
            load(1);
            loadPersons(1);
            loadLists();
            return View("Index", model);
        }
        
        //[HttpPost]
        //public ActionResult InsertCountry(List<decimal> index_row, List<decimal> area, List<String>country, List<decimal>population, List<decimal>president)
        //{
        //    var txn = (DbTransaction)TempData["txn"];
        //    TempData["txn"] = txn;
        //    var list = new ServiceReference1.ArrayOfClCountry();
        //    Dictionary<decimal, byte[]> anthems = (Dictionary<decimal,byte[]>)TempData["audio"];
        //    Dictionary<decimal, byte[]> flags = (Dictionary<decimal, byte[]>)TempData["img"];
        //    for(int i=0;i<area.Count();i++)
        //    {
        //        ServiceReference1.clCountry _country = new ServiceReference1.clCountry();
        //        //var row = countries.ElementAt(i);
        //        _country.AREA = area.ElementAt(i);
        //        _country.COUNTRYNAME = country.ElementAt(i);
        //        _country.POPULATION = population.ElementAt(i);
        //        _country.PRESIDENT = president.ElementAt(i);
        //        _country.ANTHEM = anthems[index_row.ElementAt(i)];
        //        _country.FLAG = flags[index_row.ElementAt(i)];
        //        list.Add(_country);
        //    }


        //    ws.AddCountries(list);
        //    load(1);
        //    return View("Index",model);


        //}
        [HttpPost]
        public JsonResult LoadCountry(decimal id)
        {
            var t = getTransactionNumber();
            var txn = TempData["txn"];
            var country = ws.FindCountry(id,t);
            Country p = new Country();
            p.AREA = country.AREA;
            p.COUNTRYNAME = country.COUNTRYNAME;
            p.COUNTRYID = country.COUNTRYID;
            p.POPULATION = country.POPULATION;
            p.PRESIDENT = country.PRESIDENT;
            p.FLAG = country.FLAG;
            p.FLAGB64 = country.FLAGB64;
            p.ANTHEMB64 = country.ANTHEMB64;
            TempData["txn"] = txn;
            setTransactionNumber(t);
            var j = Json(p, JsonRequestBehavior.AllowGet);
            return j;
        }

        [HttpPost]
        public JsonResult LoadPerson(decimal id)
        {
            var t = getTransactionNumber();
            var p = ws.FindPerson(id,t);
            Person person = new Person
            {
                BIRTH_COUNTRY_ID=p.BIRTH_COUNTRY_ID,
                EMAIL=p.EMAIL,
                IDENTIFICATION=p.IDENTIFICATION,
                DATE=p.BIRTH_DATE.ToString("dd/MM/yyyy"),
                NAME=p.NAME,
                PHOTOB64=p.PHOTOB64,
                RESIDENCE_COUNTRY_ID=p.RESIDENCE_COUNTRY_ID,
                VIDEO=p.VIDEO
            };
            var j = Json(person, JsonRequestBehavior.AllowGet);
            setTransactionNumber(t);
            return j;
        }
     
        public JsonResult PostFiles(HttpPostedFileBase anthem_file,HttpPostedFileBase flag_file,decimal index, decimal area, decimal population, String country, decimal president)
        {
            var t = getTransactionNumber();
            setTransactionNumber(t);
            // int t = 8;
            Dictionary<decimal, byte[]> anthems;
            Dictionary<decimal, byte[]> flags;
            anthems = (Dictionary<decimal,byte[]>)TempData["audio"];
            flags = (Dictionary<decimal, byte[]>)TempData["img"];
            if (anthems == null || index==1) anthems = new Dictionary<decimal, byte[]>();
            if (flags == null || index==1) flags = new Dictionary<decimal, byte[]>();
            byte[] anthem;
            byte[] flag;
            MemoryStream target = new MemoryStream();
            anthem_file.InputStream.CopyTo(target);
            anthem = target.ToArray();

            target = new MemoryStream();
            flag_file.InputStream.CopyTo(target);
            flag = target.ToArray();

            anthems.Add(index, anthem);
            flags.Add(index, flag);

            TempData["audio"] = anthems;
            TempData["img"] = flags;
            String row = "<tr id=row_" + index + "><td>"+country+"</td><td>"+area+"</td><td>"+population+"</td><td>"+president+ "</td>" +
                "<td><button class=\"delete-temp edit-button-table\" data-value=\""+index+"\"><i class=\"fa fa-remove\"></i></button></td>";
            return Json(row, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditCountry(String country, decimal area,decimal population,decimal countryid,decimal president,HttpPostedFileBase anthem_file, HttpPostedFileBase flag_file, decimal old_id,decimal page = 1)
        {
            byte[] anthem=null, flag=null;

            if(anthem_file!=null)
            {
                MemoryStream target = new MemoryStream();
                anthem_file.InputStream.CopyTo(target);
                anthem = target.ToArray();

            }
            if(flag_file!=null)
            {
                MemoryStream target = new MemoryStream();
                flag_file.InputStream.CopyTo(target);
                flag = target.ToArray();
            }

            var t = getTransactionNumber();
            var txn = TempData["txn"];
            TempData["txn"] = txn;
            if(country!=null)
            {
                var _country = new ServiceReference1.clCountry();
                _country.AREA = area;
                _country.COUNTRYID = countryid;
                _country.COUNTRYNAME = country;
                _country.POPULATION = population;
                _country.PRESIDENT = president;
                _country.ANTHEM = anthem;
                _country.FLAG = flag;

                var tran=ws.UpdateCountry(_country,old_id,t);
                setTransactionNumber(tran);
            }
            load(page);
            loadPersons(1);
            loadLists();
            return View("Index",model);
        }
        [HttpPost]
        public ActionResult Test1(clCountry p)
        {
            var t = getTransactionNumber();
            setTransactionNumber(t);
            p.page += 1;
            return View("Index",model);
        }

        [HttpPost]
        public ActionResult EditPerson(HttpPostedFileBase person_file, String video, String person_name, DateTime birth_date,String email,decimal identification, decimal birth_list, decimal residence_list,decimal old_id_person)
        {
            byte[] photo=null;
            MemoryStream target = new MemoryStream();
            if(person_file!=null)
            { 
                person_file.InputStream.CopyTo(target);
                photo = target.ToArray();
            }
            ServiceReference1.clPerson person = new ServiceReference1.clPerson
            {
                BIRTH_COUNTRY_ID=birth_list,
                EMAIL=email,
                BIRTH_DATE=birth_date,
                IDENTIFICATION=identification,
                NAME=person_name,
                PHOTO=photo,
                RESIDENCE_COUNTRY_ID=residence_list,
                VIDEO=video
            };
            var t = getTransactionNumber();

            t= ws.UpdatePerson(person,old_id_person,t);
            setTransactionNumber(t);
            load(1);
            loadPersons(1);
            loadLists();
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult InsertPerson(HttpPostedFileBase person_file, String video, String person_name, DateTime birth_date, String email, decimal identification, decimal birth_list, decimal residence_list)
        {
            MemoryStream target = new MemoryStream();
            person_file.InputStream.CopyTo(target);
            byte[] photo = target.ToArray();
            ServiceReference1.clPerson person = new ServiceReference1.clPerson
            {
                BIRTH_COUNTRY_ID = birth_list,
                EMAIL = email,
                BIRTH_DATE = birth_date,
                IDENTIFICATION = identification,
                NAME = person_name,
                PHOTO = photo,
                RESIDENCE_COUNTRY_ID = residence_list,
                VIDEO = video
            };
            var t = getTransactionNumber();
            t=ws.AddPerson(person,t);
            setTransactionNumber(t);
            load(1);
            loadPersons(1);
            loadLists();
            return View("Index", model);
        }

        public ActionResult DeletePerson(decimal id=-1)
        {
            if(id!=-1)
            {
                var t = getTransactionNumber();
                t=ws.deletePerson(id,t);
                setTransactionNumber(t);
            }
            load(1);
            loadPersons(1);
            loadLists();
            return View("Index", model);
        }

        public ActionResult DeleteCountry(decimal id = -1)
        {
            if (id != -1)
            {
                var t = getTransactionNumber();
                t=ws.deleteCountry(id,t);
                setTransactionNumber(t);
            }
            load(1);
            loadPersons(1);
            loadLists();
            return View("Index", model);
        }

        public ActionResult TestData()
        {
            var t = getTransactionNumber();
            setTransactionNumber(t);
            return View();
        }
        [HttpPost]
        public ActionResult Generate(decimal countries,decimal people)
        {
            var t = getTransactionNumber();
            setTransactionNumber(t);
            getTransactionNumber();
            ws.Generate_People(countries, people);
            return View("TestData");
        }

        Nullable<int> getTransactionNumber()
        {
            Nullable<int> tran = (int?)TempData["tran"];
            return tran;
        }
        void setTransactionNumber(int? tran)
        {
            TempData["tran"] = tran;
        }
        public JsonResult get_n_c()
        {
            return Json(n_c==null?1:n_c, JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_n_p()
        {
            return Json(n_p == null ? 1 : n_p, JsonRequestBehavior.AllowGet);
        }
    }
    
}