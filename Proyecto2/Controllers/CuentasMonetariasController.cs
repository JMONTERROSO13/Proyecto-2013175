using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto2.Models;
using Microsoft.AspNet.Identity;

namespace Proyecto2.Controllers
{
    public class CuentasMonetariasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /CuentasMonetarias/

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var cuentamonetarias = db.CuentaMonetarias.Include(c => c.Persona);
            return View(cuentamonetarias.ToList());
        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult MonetariasUser()
        {
            var id = User.Identity.GetUserName();
            var cuentaahorroes = db.CuentaMonetarias.Include(c => c.Persona).Where(s => s.Persona.UserName == id);
            ViewBag.persona = id;
            return View(cuentaahorroes);
        }

        // GET: /CuentasMonetarias/Details/5

        [Authorize(Roles = "Admin,User")]
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

        [Authorize(Roles = "Admin,User")]
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
        public ActionResult Create([Bind(Include="Id,NombreCuenta,Saldo,PersonaDpi")] CuentaMonetaria cuentamonetaria)
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

        [Authorize(Roles = "Admin,User")]
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
        public ActionResult Edit([Bind(Include="Id,NombreCuenta,Saldo,PersonaDpi")] CuentaMonetaria cuentamonetaria)
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

        [Authorize(Roles = "Admin,User")]
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
