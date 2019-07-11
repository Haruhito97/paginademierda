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
    public class MenuController : Controller
    {

      AlimentoVelozEntities2 ctx;

        public ActionResult Index()
        {
            ctx = new AlimentoVelozEntities2();
            var List = ctx.Productos.ToList();
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
        public ActionResult Agregar(Productos pdt)
        {
            ctx = new AlimentoVelozEntities2();
            var exist = ctx.Productos.Find(pdt.id_producto);
            if (exist == null)
            {
                ctx = new AlimentoVelozEntities2();
                ctx.Productos.Add(pdt);
                ctx.SaveChanges();
                TempData["shortMessage"] = "Producto Agregado Exitosamente";
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
            var pdt = ctx.Productos.Find(id);
            if (pdt == null)
            {
                TempData["shortMessage"] = "No se encontro el Producto";
                return RedirectToAction("Index");
            }
            return View(pdt);
        }

        public ActionResult Editar(String id)
        {
            ctx = new AlimentoVelozEntities2();
            var pdt = ctx.Productos.Find(id);
            return View(pdt);
        }

        [HttpPost]
        public ActionResult Editar(Productos pdt)
        {
            ctx = new AlimentoVelozEntities2();
            ctx.Entry(pdt).State = EntityState.Modified;
            ctx.SaveChanges();
            TempData["shortMessage"] = "Guardado Exitoso";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(String id)
        {
            ctx = new AlimentoVelozEntities2();
            var pdt = from c in ctx.Productos
                      select c;

            if (!String.IsNullOrEmpty(id))
            {
                pdt = pdt.Where(s => s.id_producto.Contains(id));
            }
            return View(pdt);
        }


        public ActionResult Delete(String id)
        {
            ctx = new AlimentoVelozEntities2();
            if (id == null)
            {
                TempData["shortMessage"] = "El id no puede ser null";
                return RedirectToAction("Index");
            }
            var pdt = ctx.Productos.Find(id);
            if (pdt == null)
            {
                TempData["shortMessage"] = "El id ya no existe";
                return RedirectToAction("Index");
            }
            else
            {
                return View(pdt);
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
                var pdt = ctx.Productos.Find(id);
                if(pdt == null){
                    TempData["shortMessage"] = "el id ya no existe";
                    return RedirectToAction("Index");
                }
                else
                {
                    try
                    {
                        ctx.Productos.Remove(pdt);
                        ctx.SaveChanges();
                        TempData["shortMessage"] = "Producto eliminado con exito";
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
