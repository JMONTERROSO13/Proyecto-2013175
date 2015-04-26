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
    public class TarjetasDebitoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /TarjetasDebito/
        public ActionResult Index()
        {
            var tarjetadebitoes = db.TarjetaDebitoes.Include(t => t.CuentaMonetaria);
            return View(tarjetadebitoes.ToList());
        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult TajetasUser(int id)  
        {
            
            var tarjetas = db.TarjetaDebitoes.Include(c => c.CuentaMonetaria).Where(s => s.CuentaMonetariaId == id);
            return View(tarjetas);  
         }


        // GET: /TarjetasDebito/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarjetaDebito tarjetadebito = db.TarjetaDebitoes.Find(id);
            if (tarjetadebito == null)
            {
                return HttpNotFound();
            }
            return View(tarjetadebito);
        }

        // GET: /TarjetasDebito/Create
        public ActionResult Create()
        {
            ViewBag.CuentaMonetariaId = new SelectList(db.CuentaMonetarias, "Id", "NombreCuenta");
            return View();
        }

        // POST: /TarjetasDebito/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,NumeroTarjeta,Estado,Credito,CuentaMonetariaSaldo,CuentaMonetariaId,Pin")] TarjetaDebito tarjetadebito)
        {
            if (ModelState.IsValid)
            {
                db.TarjetaDebitoes.Add(tarjetadebito);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CuentaMonetariaId = new SelectList(db.CuentaMonetarias, "Id", "NombreCuenta", tarjetadebito.CuentaMonetariaId);
            return View(tarjetadebito);
        }

        // GET: /TarjetasDebito/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarjetaDebito tarjetadebito = db.TarjetaDebitoes.Find(id);
            if (tarjetadebito == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuentaMonetariaId = new SelectList(db.CuentaMonetarias, "Id", "NombreCuenta", tarjetadebito.CuentaMonetariaId);
            return View(tarjetadebito);
        }

        // POST: /TarjetasDebito/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,NumeroTarjeta,Estado,Credito,CuentaMonetariaSaldo,CuentaMonetariaId,Pin")] TarjetaDebito tarjetadebito)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarjetadebito).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CuentaMonetariaId = new SelectList(db.CuentaMonetarias, "Id", "NombreCuenta", tarjetadebito.CuentaMonetariaId);
            return View(tarjetadebito);
        }

        // GET: /TarjetasDebito/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TarjetaDebito tarjetadebito = db.TarjetaDebitoes.Find(id);
            if (tarjetadebito == null)
            {
                return HttpNotFound();
            }
            return View(tarjetadebito);
        }

        // POST: /TarjetasDebito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TarjetaDebito tarjetadebito = db.TarjetaDebitoes.Find(id);
            db.TarjetaDebitoes.Remove(tarjetadebito);
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
