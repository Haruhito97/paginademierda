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
    public class LocalesController : Controller
    {
        
        AlimentoVelozEntities2 ctx;

        public ActionResult Listalocales()
        {
            ctx = new AlimentoVelozEntities2();
            var listaLocales = ctx.locales.ToList();
            try
            {
                ViewBag.ErrorMessage = TempData["shortMessage"].ToString();
                return View(listaLocales);
            }
            catch
            {
                return View(listaLocales);
            }
            
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(locales loc)
        {
            ctx = new AlimentoVelozEntities2();
            var exist = ctx.locales.Find(loc.id_local);
            var gnt = ctx.gerente.Find(loc.id_gerente);
            if(exist == null ){
                if (gnt != null){
                    ctx = new AlimentoVelozEntities2();
                    ctx.locales.Add(loc);
                    ctx.SaveChanges();
                    TempData["shortMessage"] = "Usuario Agregado Exitosamente";
                    return RedirectToAction("ListaLocales");
                }
                else
                {
                    ViewBag.ErrorMessage = "el ID de gerente ingresado no existe";
                    return View();
                }
            }else{
                ViewBag.ErrorMessage = "el ID que está ingresando ya existe";
                return View();
            }
        }

        public ActionResult Details(String id)
        {
            ctx = new AlimentoVelozEntities2();
            if(id == null || id == "" ){
                TempData["shortMessage"] = "el ID no puede ser null";
                return RedirectToAction("ListaLocales");
            }
            else { }
            var local = ctx.locales.Find(id);
            if (local == null)
            {
                TempData["shortMessage"] = "No se encontro el local";
                return RedirectToAction("ListaLocales");
            }
            return View(local);
        }

        public ActionResult Editar(String id)
        {
            ctx = new AlimentoVelozEntities2();
            var loc = ctx.locales.Find(id);
            return View(loc);
        }

        [HttpPost]
        public ActionResult Editar(locales loc)
        {
            ctx = new AlimentoVelozEntities2();
            ctx.Entry(loc).State = EntityState.Modified;
            ctx.SaveChanges();
            TempData["shortMessage"] = "Guardado Exitoso";
            return RedirectToAction("ListaLocales");
        }

        [HttpPost]
        public ActionResult Listalocales(String id)
        {
            ctx = new AlimentoVelozEntities2();
            var locales = from c in ctx.locales
                         select c;

            if (!String.IsNullOrEmpty(id))
            {
                locales = locales.Where(s => s.id_local.Contains(id));
            }
            return View(locales);
        }


        public ActionResult Delete(String id)
        {
            ctx = new AlimentoVelozEntities2();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var local = ctx.locales.Find(id);
            var id_local = local.id_local;
            var repartidores = from c in ctx.repartidor
                               select c;
            repartidores = repartidores.Where(s => s.id_local.Equals(id_local));
            if (repartidores.Count() > 0)
            {
                TempData["shortMessage"] = "Existen coincidencias de FKs, debera eliminarlas antes de proceder";
                return RedirectToAction("ListaLocales");
            }
            else
            {
                if (local == null)
                {
                    TempData["shortMessage"] = "El id ya no existe";
                    return RedirectToAction("ListaLocales");
                }
                else
                {
                    return View(local);
                }
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(String id)
        {
            ctx = new AlimentoVelozEntities2();
            if (id != null)
            {
                var loc = ctx.locales.Find(id);
                if (loc != null)
                {
                    try
                    {
                        ctx.locales.Remove(loc);
                        ctx.SaveChanges();
                        TempData["shortMessage"] = "Local borrado con Exito";
                        return RedirectToAction("ListaLocales");
                    }
                    catch
                    {
                        TempData["shortMessage"] = "No se pudo completar la operacion";
                        return RedirectToAction("ListaLocales");
                    }
                }
                else
                {

                    TempData["shortMessage"] = "El ID que està tratando de eliminar no existe";
                    return RedirectToAction("ListaLocales");
                }
            }
            else
            {
                TempData["shortMessage"] = "El ID que està tratando de eliminar no existe";
                return RedirectToAction("ListaLocales");
            }
        }
        
    }
}
