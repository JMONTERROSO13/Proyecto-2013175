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
    public class PagoCreditoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /PagoCredito/
        public ActionResult Index()
        {
            var pagocreditoes = db.PagoCreditoes.Include(p => p.TarjetaCredito);
            return View(pagocreditoes.ToList());
        }

        // GET: /PagoCredito/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoCredito pagocredito = db.PagoCreditoes.Find(id);
            if (pagocredito == null)
            {
                return HttpNotFound();
            }
            return View(pagocredito);
        }

        // GET: /PagoCredito/Create
        public ActionResult Create()
        {
            ViewBag.TarjetaCreditoId = new SelectList(db.TarjetaCreditoes, "Id", "NumeroTarjeta");
            return View();
        }

        // POST: /PagoCredito/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,TarjetaCreditoCredito,TarjetaCreditoId,Descripcion,Monto")] PagoCredito pagocredito)
        {
            if (ModelState.IsValid)
            {
                db.PagoCreditoes.Add(pagocredito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TarjetaCreditoId = new SelectList(db.TarjetaCreditoes, "Id", "NumeroTarjeta", pagocredito.TarjetaCreditoId);
            return View(pagocredito);
        }

        // GET: /PagoCredito/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoCredito pagocredito = db.PagoCreditoes.Find(id);
            if (pagocredito == null)
            {
                return HttpNotFound();
            }
            ViewBag.TarjetaCreditoId = new SelectList(db.TarjetaCreditoes, "Id", "NumeroTarjeta", pagocredito.TarjetaCreditoId);
            return View(pagocredito);
        }

        // POST: /PagoCredito/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,TarjetaCreditoCredito,TarjetaCreditoId,Descripcion,Monto")] PagoCredito pagocredito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagocredito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TarjetaCreditoId = new SelectList(db.TarjetaCreditoes, "Id", "NumeroTarjeta", pagocredito.TarjetaCreditoId);
            return View(pagocredito);
        }

        // GET: /PagoCredito/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PagoCredito pagocredito = db.PagoCreditoes.Find(id);
            if (pagocredito == null)
            {
                return HttpNotFound();
            }
            return View(pagocredito);
        }

        // POST: /PagoCredito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PagoCredito pagocredito = db.PagoCreditoes.Find(id);
            db.PagoCreditoes.Remove(pagocredito);
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
