using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestFS.Models;
using TestFS.ServiceReference1;
namespace TestFS.Controllers
{
    public class QueriesController : Controller
    {
        clQuery model = new clQuery();
        ServiceReference1.SolicitudSoapClient ws = new ServiceReference1.SolicitudSoapClient();
        // GET: Queries
        private void LoadPeopleYearAll(Int64 page)
        {
            clQuery model = new clQuery();
            model.page_2 = page;
            foreach(var row in ws.PEOPLE_YEAR_ALLCOUNTRIES(page,15))
            {
                model.people_year_all.Add(new Country
                {
                    AREA=row.AREA,
                    POPULATION=row.POPULATION
                });
            }
            if (this.model != null) { this.model.people_year_all = model.people_year_all; model.page_2 = page; }
            else { this.model = model; }
        }
        private void LoadPeopleYear(decimal id,Int64 page)
        {
            clQuery model = new clQuery();
            model.page_1 = page;
            foreach (var row in ws.PEOPLE_YEAR_COUNTRY(id,page, 15))
            {
                model.people_year.Add(new Country
                {
                    AREA = row.AREA,
                    POPULATION = row.POPULATION
                });
            }
            if (this.model != null) { this.model.people_year = model.people_year; model.page_1 = page; }
            else { this.model = model; }
        }
        private List<Country> LoadInfo(Int64 page)
        {
            List<Country> list = new List<Country>();
            foreach (var row in ws.GET_COUNTRIES_INFO(page, 15))
            {
               list.Add(new Country
                {
                    COUNTRYNAME=row.COUNTRYNAME,
                    COUNTRYID=row.COUNTRYID,
                    AREA=row.AREA,
                    POPULATION=row.POPULATION
                });
            }
            return list;
        }
        public void loadLists()
        {
            ViewBag.country_1 = new SelectList(ws.GetPaisesLista(null), "COUNTRYID", "COUNTRYNAME");
            //ViewBag.residence_list = new SelectList(ws.GetPaisesLista(), "COUNTRYID", "COUNTRYNAME");
        }
        public ActionResult NavigatePeopleYearAll(Int64 page)
        {
            LoadPeopleYearAll(page);
            if(Request.IsAjaxRequest())
            {
                return PartialView("_PeopleYear", model.people_year_all);
            }
            loadLists();
            return View(model);
        }
        public ActionResult NavigatePeopleYear(decimal id,Int64 page)
        {
            LoadPeopleYear(id,page);
            if (Request.IsAjaxRequest())
            {
                //var view = PartialView("")
                return PartialView("_PeopleYear", model.people_year);
            }
            LoadPeopleYearAll(1);
            loadLists();
            return View(model);
        }
        public ActionResult NavigateInfo(Int64 page)
        {
            if (page <= 0) page = 1;
            var list = LoadInfo(page);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_CountryInfo", list);
            }
            return View(list);
        }
        public ActionResult Main()
        {
            LoadPeopleYearAll(1);
            loadLists();
            return View(model);
        }
        public ActionResult CountryInfo()
        {
            var l = LoadInfo(1);
            return View(l);
        }

        public ActionResult QueryII()
        {
            var l = ws.query_II();
            var list = new TestFS.Models.ResultList();

            foreach(var row in l.list)
            {
                list.list.Add(new Models.Result {
                    count=row.count,
                    countryid=row.countryid,
                    countryname=row.countryname,
                    year=row.year
                });
            }
            list.max = l.max;
            list.min = l.min;
            return View(list);
        }
    }
}