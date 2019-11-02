using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using qwerty1.Models;

namespace qwerty1.Controllers
{
    public class AirlineController : Controller
    {
        private CrudOpContext orm = null;
        public AirlineController(CrudOpContext _orm)
        {
            orm = _orm;
        }
        [HttpGet]
        public IActionResult AddNewAirline()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewAirline(Airline A)
        {
            try
            {
                orm.Airline.Add(A);
                orm.SaveChanges();
                ViewBag.Message = A.Name + "Airline Saved Successfully!";
                ViewBag.Messagetype = "S";

            }
            catch
            {
                ViewBag.Message = A.Name + "Error in Saving Airline!";
                ViewBag.Messagetype = "E";
            }
            return View();
        }

        public string GetVideoAd()
        {
            return "<iframe width='560' height='315' src='https://www.youtube.com/embed/yAIALB5VT2c' frameborder='0' allow='accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture' allowfullscreen></iframe>";
        }



       public IActionResult AllAirline(string f)
        {
            try
            {
                if (HttpContext.Session.GetString("Role") != "Admin")
                {
                    return RedirectToAction("Login", "SystemUsers");
                }
            }
            catch
            {
                return RedirectToAction("Login", "SystemUsers");
            }

            if (f =="all")
            {

                var d = orm.Airline.ToList();
                return View(d);
            }
            
            
            var searchdata = orm.Airline.Where(abc => abc.Name.Contains(f) || abc.Country.Contains(f)).ToList();

                return View(searchdata);

            
            
        }

        public string DeleteAirline(int id)
        {
            Airline A = orm.Airline.Find(id);

            orm.Airline.Remove(A);
            orm.SaveChanges();
            return "1";
        }

        [HttpGet]
        public IActionResult EditAirline(int id)
        {
            var A = orm.Airline.Find(id);
            return View(A);
        }

        [HttpPost]
        public IActionResult EditAirline(Airline A)
        {
            orm.Airline.Update(A);
            orm.SaveChanges();
            return RedirectToAction("AllAirline");

        }

        public IActionResult ViewAirlineDetail(int id)
        {
            return View(orm.Airline.Find(id));
        }


    }
}