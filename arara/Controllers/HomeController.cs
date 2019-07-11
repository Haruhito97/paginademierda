using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arara.Models;

namespace arara.Controllers
{
    public class HomeController : Controller
    {
        AlimentoVelozEntities2 ctx;
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IntraHome()
        {
            ViewBag.Message = "Servicio de Gestion Interna";

            return View();
        }

        public ActionResult Ubicacion()
        {
            ctx = new AlimentoVelozEntities2();
            var loc = ctx.locales.ToList();
            ViewBag.Message = "Tenemos Locales en :";
            return View(loc);
        }

        public ActionResult Contacto()
        {

            return View();
        }

        public ActionResult About()
        {

            return View();
        }
        
    }
}
