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
    public class CuentasAhorroController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /CuentasAhorro/
        public ActionResult Index()
        {
            var cuentaahorroes = db.CuentaAhorroes.Include(c => c.Persona);
            return View(cuentaahorroes.ToList());
        }

        public ActionResult AhorroUser()
        {
            var id = User.Identity.GetUserName();
            var cuentaahorroes = db.CuentaAhorroes.Include(c => c.Persona).Where(s => s.Persona.UserName == id);
            return View(cuentaahorroes);
        }

        // GET: /CuentasAhorro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaAhorro cuentaahorro = db.CuentaAhorroes.Find(id);
            if (cuentaahorro == null)
            {
                return HttpNotFound();
            }
            return View(cuentaahorro);
        }

        // GET: /CuentasAhorro/Create
        public ActionResult Create()
        {
            ViewBag.PersonaDpi = new SelectList(db.Personas, "Dpi", "Nombre");
            return View();
        }

        // POST: /CuentasAhorro/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,nombreCuenta,saldo,PersonaDpi")] CuentaAhorro cuentaahorro)
        {
            if (ModelState.IsValid)
            {
                db.CuentaAhorroes.Add(cuentaahorro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonaDpi = new SelectList(db.Personas, "Dpi", "Nombre", cuentaahorro.PersonaDpi);
            return View(cuentaahorro);
        }

        // GET: /CuentasAhorro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaAhorro cuentaahorro = db.CuentaAhorroes.Find(id);
            if (cuentaahorro == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonaDpi = new SelectList(db.Personas, "Dpi", "Nombre", cuentaahorro.PersonaDpi);
            return View(cuentaahorro);
        }

        // POST: /CuentasAhorro/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,nombreCuenta,saldo,PersonaDpi")] CuentaAhorro cuentaahorro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuentaahorro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonaDpi = new SelectList(db.Personas, "Dpi", "Nombre", cuentaahorro.PersonaDpi);
            return View(cuentaahorro);
        }

        // GET: /CuentasAhorro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaAhorro cuentaahorro = db.CuentaAhorroes.Find(id);
            if (cuentaahorro == null)
            {
                return HttpNotFound();
            }
            return View(cuentaahorro);
        }

        // POST: /CuentasAhorro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CuentaAhorro cuentaahorro = db.CuentaAhorroes.Find(id);
            db.CuentaAhorroes.Remove(cuentaahorro);
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
