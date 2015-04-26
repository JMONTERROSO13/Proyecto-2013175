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
    public class RetiroDebitoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /RetiroDebito/
        public ActionResult Index()
        {
            var retirodebitoes = db.RetiroDebitoes.Include(r => r.TarjetaDebito);
            return View(retirodebitoes.ToList());
        }

        // GET: /RetiroDebito/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RetiroDebito retirodebito = db.RetiroDebitoes.Find(id);
            if (retirodebito == null)
            {
                return HttpNotFound();
            }
            return View(retirodebito);
        }

        // GET: /RetiroDebito/Create
        public ActionResult Create()
        {
            ViewBag.TarjetaDebitoId = new SelectList(db.TarjetaDebitoes, "Id", "NumeroTarjeta");
            return View();
        }

        // POST: /RetiroDebito/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,TarjetaDebitoId,TarjetaDebitoCredito,Descripcion,Monto")] RetiroDebito retirodebito)
        {
            if (ModelState.IsValid)
            {
                db.RetiroDebitoes.Add(retirodebito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TarjetaDebitoId = new SelectList(db.TarjetaDebitoes, "Id", "NumeroTarjeta", retirodebito.TarjetaDebitoId);
            return View(retirodebito);
        }

        // GET: /RetiroDebito/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RetiroDebito retirodebito = db.RetiroDebitoes.Find(id);
            if (retirodebito == null)
            {
                return HttpNotFound();
            }
            ViewBag.TarjetaDebitoId = new SelectList(db.TarjetaDebitoes, "Id", "NumeroTarjeta", retirodebito.TarjetaDebitoId);
            return View(retirodebito);
        }

        // POST: /RetiroDebito/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,TarjetaDebitoId,TarjetaDebitoCredito,Descripcion,Monto")] RetiroDebito retirodebito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(retirodebito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TarjetaDebitoId = new SelectList(db.TarjetaDebitoes, "Id", "NumeroTarjeta", retirodebito.TarjetaDebitoId);
            return View(retirodebito);
        }

        // GET: /RetiroDebito/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RetiroDebito retirodebito = db.RetiroDebitoes.Find(id);
            if (retirodebito == null)
            {
                return HttpNotFound();
            }
            return View(retirodebito);
        }

        // POST: /RetiroDebito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RetiroDebito retirodebito = db.RetiroDebitoes.Find(id);
            db.RetiroDebitoes.Remove(retirodebito);
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
