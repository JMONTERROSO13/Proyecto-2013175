using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto2.Models;

namespace Proyecto2.Controllers
{
    public class CargoCreditoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /CargoCredito/
        public ActionResult Index()
        {
            var cargocreditoes = db.CargoCreditoes.Include(c => c.CuentaMonetaria);
            return View(cargocreditoes.ToList());
        }

        // GET: /CargoCredito/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoCredito cargocredito = db.CargoCreditoes.Find(id);
            if (cargocredito == null)
            {
                return HttpNotFound();
            }
            return View(cargocredito);
        }

        // GET: /CargoCredito/Create
        public ActionResult Create()
        {
            ViewBag.CuentaMonetariaId = new SelectList(db.CuentaMonetarias, "Id", "NombreCuenta");
            return View();
        }

        // POST: /CargoCredito/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,CuentaMonetariaSaldo,CuentaMonetariaId,Descripcion,Monto")] CargoCredito cargocredito)
        {
            if (ModelState.IsValid)
            {
                db.CargoCreditoes.Add(cargocredito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CuentaMonetariaId = new SelectList(db.CuentaMonetarias, "Id", "NombreCuenta", cargocredito.CuentaMonetariaId);
            return View(cargocredito);
        }

        // GET: /CargoCredito/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoCredito cargocredito = db.CargoCreditoes.Find(id);
            if (cargocredito == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuentaMonetariaId = new SelectList(db.CuentaMonetarias, "Id", "NombreCuenta", cargocredito.CuentaMonetariaId);
            return View(cargocredito);
        }

        // POST: /CargoCredito/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,CuentaMonetariaSaldo,CuentaMonetariaId,Descripcion,Monto")] CargoCredito cargocredito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargocredito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CuentaMonetariaId = new SelectList(db.CuentaMonetarias, "Id", "NombreCuenta", cargocredito.CuentaMonetariaId);
            return View(cargocredito);
        }

        // GET: /CargoCredito/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoCredito cargocredito = db.CargoCreditoes.Find(id);
            if (cargocredito == null)
            {
                return HttpNotFound();
            }
            return View(cargocredito);
        }

        // POST: /CargoCredito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CargoCredito cargocredito = db.CargoCreditoes.Find(id);
            db.CargoCreditoes.Remove(cargocredito);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
