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
    public class TarjetasCreditoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /TarjetasCredito/
        public ActionResult Index()
        {
            var tarjetacreditoes = db.TarjetaCreditoes.Include(t => t.CuentaMonetaria);
            return View(tarjetacreditoes.ToList());
        }

        // GET: /TarjetasCredito/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarjetaCredito tarjetacredito = db.TarjetaCreditoes.Find(id);
            if (tarjetacredito == null)
            {
                return HttpNotFound();
            }
            return View(tarjetacredito);
        }

        // GET: /TarjetasCredito/Create
        public ActionResult Create()
        {
            ViewBag.CuentaMonetariaId = new SelectList(db.CuentaMonetarias, "Id", "NombreCuenta");
            return View();
        }

        // POST: /TarjetasCredito/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,NumeroTarjeta,Estado,Credito,CuentaAhorroSaldo,CuentaMonetariaSaldo,CuentaMonetariaId,Pin")] TarjetaCredito tarjetacredito)
        {
            if (ModelState.IsValid)
            {
                db.TarjetaCreditoes.Add(tarjetacredito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CuentaMonetariaId = new SelectList(db.CuentaMonetarias, "Id", "NombreCuenta", tarjetacredito.CuentaMonetariaId);
            return View(tarjetacredito);
        }

        // GET: /TarjetasCredito/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarjetaCredito tarjetacredito = db.TarjetaCreditoes.Find(id);
            if (tarjetacredito == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuentaMonetariaId = new SelectList(db.CuentaMonetarias, "Id", "NombreCuenta", tarjetacredito.CuentaMonetariaId);
            return View(tarjetacredito);
        }

        // POST: /TarjetasCredito/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,NumeroTarjeta,Estado,Credito,CuentaAhorroSaldo,CuentaMonetariaSaldo,CuentaMonetariaId,Pin")] TarjetaCredito tarjetacredito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarjetacredito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CuentaMonetariaId = new SelectList(db.CuentaMonetarias, "Id", "NombreCuenta", tarjetacredito.CuentaMonetariaId);
            return View(tarjetacredito);
        }

        // GET: /TarjetasCredito/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarjetaCredito tarjetacredito = db.TarjetaCreditoes.Find(id);
            if (tarjetacredito == null)
            {
                return HttpNotFound();
            }
            return View(tarjetacredito);
        }

        // POST: /TarjetasCredito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TarjetaCredito tarjetacredito = db.TarjetaCreditoes.Find(id);
            db.TarjetaCreditoes.Remove(tarjetacredito);
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
