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
    public class RetiroAhorroController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /RetiroAhorro/
        public ActionResult Index()
        {
            var retiroahorroes = db.RetiroAhorroes.Include(r => r.CuentaAhorro);
            return View(retiroahorroes.ToList());
        }

        // GET: /RetiroAhorro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RetiroAhorro retiroahorro = db.RetiroAhorroes.Find(id);
            if (retiroahorro == null)
            {
                return HttpNotFound();
            }
            return View(retiroahorro);
        }

        // GET: /RetiroAhorro/Create
        public ActionResult Create()
        {
            ViewBag.CuentaAhorroId = new SelectList(db.CuentaAhorroes, "Id", "NombreCuenta");
            return View();
        }

        // POST: /RetiroAhorro/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,CuentaAhorroId,CuentaAhorroSaldo,Descripcion,Monto")] RetiroAhorro retiroahorro)
        {
            if (ModelState.IsValid)
            {
                db.RetiroAhorroes.Add(retiroahorro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CuentaAhorroId = new SelectList(db.CuentaAhorroes, "Id", "NombreCuenta", retiroahorro.CuentaAhorroId);
            return View(retiroahorro);
        }

        // GET: /RetiroAhorro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RetiroAhorro retiroahorro = db.RetiroAhorroes.Find(id);
            if (retiroahorro == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuentaAhorroId = new SelectList(db.CuentaAhorroes, "Id", "NombreCuenta", retiroahorro.CuentaAhorroId);
            return View(retiroahorro);
        }

        // POST: /RetiroAhorro/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,CuentaAhorroId,CuentaAhorroSaldo,Descripcion,Monto")] RetiroAhorro retiroahorro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(retiroahorro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CuentaAhorroId = new SelectList(db.CuentaAhorroes, "Id", "NombreCuenta", retiroahorro.CuentaAhorroId);
            return View(retiroahorro);
        }

        // GET: /RetiroAhorro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RetiroAhorro retiroahorro = db.RetiroAhorroes.Find(id);
            if (retiroahorro == null)
            {
                return HttpNotFound();
            }
            return View(retiroahorro);
        }

        // POST: /RetiroAhorro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RetiroAhorro retiroahorro = db.RetiroAhorroes.Find(id);
            db.RetiroAhorroes.Remove(retiroahorro);
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
