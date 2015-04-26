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
    public class AbonoDebitoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /AbonoDebito/
        public ActionResult Index()
        {
            var abonodebitoes = db.AbonoDebitoes.Include(a => a.TarjetaDebito);
            return View(abonodebitoes.ToList());
        }

        // GET: /AbonoDebito/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AbonoDebito abonodebito = db.AbonoDebitoes.Find(id);
            if (abonodebito == null)
            {
                return HttpNotFound();
            }
            return View(abonodebito);
        }

        // GET: /AbonoDebito/Create
        public ActionResult Create()
        {
            ViewBag.TarjetaDebitoId = new SelectList(db.TarjetaDebitoes, "Id", "NumeroTarjeta");
            return View();
        }

        // POST: /AbonoDebito/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,TarjetaDebitoId,TarjetaDebitoCredito,Descripcion,Monto")] AbonoDebito abonodebito)
        {
            if (ModelState.IsValid)
            {
                db.AbonoDebitoes.Add(abonodebito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TarjetaDebitoId = new SelectList(db.TarjetaDebitoes, "Id", "NumeroTarjeta", abonodebito.TarjetaDebitoId);
            return View(abonodebito);
        }

        // GET: /AbonoDebito/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AbonoDebito abonodebito = db.AbonoDebitoes.Find(id);
            if (abonodebito == null)
            {
                return HttpNotFound();
            }
            ViewBag.TarjetaDebitoId = new SelectList(db.TarjetaDebitoes, "Id", "NumeroTarjeta", abonodebito.TarjetaDebitoId);
            return View(abonodebito);
        }

        // POST: /AbonoDebito/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,TarjetaDebitoId,TarjetaDebitoCredito,Descripcion,Monto")] AbonoDebito abonodebito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(abonodebito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TarjetaDebitoId = new SelectList(db.TarjetaDebitoes, "Id", "NumeroTarjeta", abonodebito.TarjetaDebitoId);
            return View(abonodebito);
        }

        // GET: /AbonoDebito/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AbonoDebito abonodebito = db.AbonoDebitoes.Find(id);
            if (abonodebito == null)
            {
                return HttpNotFound();
            }
            return View(abonodebito);
        }

        // POST: /AbonoDebito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AbonoDebito abonodebito = db.AbonoDebitoes.Find(id);
            db.AbonoDebitoes.Remove(abonodebito);
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
