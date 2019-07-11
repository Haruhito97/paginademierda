using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using arara.Models;
using System.Data;
using System.Net;

namespace arara.Controllers
{
    public class GerentesController : Controller
    {

        AlimentoVelozEntities2 ctx;

        public ActionResult Index()
        {
            ctx = new AlimentoVelozEntities2();
            var List = ctx.gerente.ToList();
            try
            {
                ViewBag.ErrorMessage = TempData["shortMessage"].ToString();
                return View(List);
            }
            catch
            {
                return View(List);
            }
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(gerente gnt)
        {
            ctx = new AlimentoVelozEntities2();
            var exist = ctx.gerente.Find(gnt.id_gerente);
            if (exist == null)
            {
                ctx = new AlimentoVelozEntities2();
                ctx.gerente.Add(gnt);
                ctx.SaveChanges();
                TempData["shortMessage"] = "Gerente Agregado Exitosamente";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "el ID que está ingresando ya existe";
                return View();
            }
        }

        public ActionResult Details(String id)
        {
            ctx = new AlimentoVelozEntities2();
            if (id == null || id == "")
            {
                TempData["shortMessage"] = "el ID no puede ser null";
                return RedirectToAction("Index");
            }
            var gnt = ctx.gerente.Find(id);
            if (gnt == null)
            {
                TempData["shortMessage"] = "No se encontro el gerente";
                return RedirectToAction("Index");
            }
            return View(gnt);
        }

        public ActionResult Editar(String id)
        {
            ctx = new AlimentoVelozEntities2();
            var gnt = ctx.gerente.Find(id);
            return View(gnt);
        }

        [HttpPost]
        public ActionResult Editar(gerente gnt)
        {
            ctx = new AlimentoVelozEntities2();
            ctx.Entry(gnt).State = EntityState.Modified;
            ctx.SaveChanges();
            TempData["shortMessage"] = "Guardado Exitoso";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(String id)
        {
            ctx = new AlimentoVelozEntities2();
            var gnt = from c in ctx.gerente
                          select c;

            if (!String.IsNullOrEmpty(id))
            {
                gnt = gnt.Where(s => s.id_gerente.Contains(id));
            }
            return View(gnt);
        }


        public ActionResult Delete(String id)
        {
            ctx = new AlimentoVelozEntities2();
            if (id == null)
            {
                TempData["shortMessage"] = "El id no puede ser null";
                return RedirectToAction("Index");
            }
            var gnt = ctx.gerente.Find(id);
            var id_gerente = gnt.id_gerente;
            var lista = from c in ctx.locales
                          select c;
            lista = lista.Where(s => s.id_gerente.Equals(id_gerente));
            if(lista.Count() > 0){
                TempData["shortMessage"] = "Existen coincidencias de FKs, debera eliminarlas antes de proceder";
                return RedirectToAction("ListaLocales");
            }
            else
            {
                if (gnt == null)
                {
                    TempData["shortMessage"] = "El id ya no existe";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(gnt);
                }
            }
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(String id)
        {
            ctx = new AlimentoVelozEntities2();
            if (id == null)
            {
                TempData["shortMessage"] = "el id no puede ser null o ya no existe";
                return RedirectToAction("Index");
            }
            else
            {
                var gnt = ctx.gerente.Find(id);
                if (gnt == null){
                    TempData["shortMessage"] = "el id ya no existe";
                    return RedirectToAction("Index");
                }
                else
                {
                    try
                    {
                        ctx.gerente.Remove(gnt);
                        ctx.SaveChanges();
                        TempData["shortMessage"] = "Gerente eliminado con exito";
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        TempData["shortMessage"] = "No se pudo completar la operacion";
                        return RedirectToAction("Index");
                    }

                }
            }
        }
        
    }
}
