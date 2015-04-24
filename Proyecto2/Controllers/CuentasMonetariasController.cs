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
    public class CuentasMonetariasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /CuentasMonetarias/
        public ActionResult Index()
        {
            var cuentamonetarias = db.CuentaMonetarias.Include(c => c.Persona);
            return View(cuentamonetarias.ToList());
        }

        // GET: /CuentasMonetarias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaMonetaria cuentamonetaria = db.CuentaMonetarias.Find(id);
            if (cuentamonetaria == null)
            {
                return HttpNotFound();
            }
            return View(cuentamonetaria);
        }

        // GET: /CuentasMonetarias/Create
        public ActionResult Create()
        {
            ViewBag.PersonaDpi = new SelectList(db.Personas, "Dpi", "Nombre");
            return View();
        }

        // POST: /CuentasMonetarias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,nombreCuenta,saldo,PersonaDpi")] CuentaMonetaria cuentamonetaria)
        {
            if (ModelState.IsValid)
            {
                db.CuentaMonetarias.Add(cuentamonetaria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonaDpi = new SelectList(db.Personas, "Dpi", "Nombre", cuentamonetaria.PersonaDpi);
            return View(cuentamonetaria);
        }

        // GET: /CuentasMonetarias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaMonetaria cuentamonetaria = db.CuentaMonetarias.Find(id);
            if (cuentamonetaria == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonaDpi = new SelectList(db.Personas, "Dpi", "Nombre", cuentamonetaria.PersonaDpi);
            return View(cuentamonetaria);
        }

        // POST: /CuentasMonetarias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,nombreCuenta,saldo,PersonaDpi")] CuentaMonetaria cuentamonetaria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuentamonetaria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonaDpi = new SelectList(db.Personas, "Dpi", "Nombre", cuentamonetaria.PersonaDpi);
            return View(cuentamonetaria);
        }

        // GET: /CuentasMonetarias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaMonetaria cuentamonetaria = db.CuentaMonetarias.Find(id);
            if (cuentamonetaria == null)
            {
                return HttpNotFound();
            }
            return View(cuentamonetaria);
        }

        // POST: /CuentasMonetarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CuentaMonetaria cuentamonetaria = db.CuentaMonetarias.Find(id);
            db.CuentaMonetarias.Remove(cuentamonetaria);
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
