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
using System.Threading.Tasks;

namespace Proyecto2.Controllers
{
    public class PrestamosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Prestamos/
        public ActionResult Index()
        {
            var prestamoes = db.Prestamoes.Include(p => p.CuentaMonetaria);
            return View(prestamoes.ToList());
        }

        // GET: /Prestamos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamoes.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // GET: /Prestamos/Create
        public ActionResult Create(string  names, int Saldo, int id)
        {
            var nombre = User.Identity.GetUserName();

            ViewBag.nombre = nombre;
            ViewBag.nombres = names;
            ViewBag.idCuenta = id;
            ViewBag.saldoCuenta = Saldo;
            ViewBag.CuentaMonetariaId = new SelectList(db.CuentaMonetarias, "Id", "NombreCuenta");
            return View();
        }

        // POST: /Prestamos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        //[Authorize(Roles = "Admin")]       
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Prestamo prestamo) 
        {
            if (ModelState.IsValid)
            {
                var prestamos = new Prestamo()
                {
                    CuentaMonetariaId = prestamo.CuentaMonetariaId,
                    Monto = prestamo.Monto,
                    CuentaMonetariaSaldo = prestamo.CuentaMonetariaSaldo,
                    Descripcion = prestamo.Descripcion
                };

                if (ModelState.IsValid)
                {
                    int cutID = prestamo.CuentaMonetariaId;
                    var cuenta = db.CuentaMonetarias.FirstOrDefault(p => p.Id == cutID);

                    int saldo = cuenta.Saldo;
                    int prestar = prestamo.Monto;
                    if (saldo >= prestar)
                    {
                        var nuevoSaldo = saldo - prestar;
                        cuenta.Saldo = nuevoSaldo;
                        db.Prestamoes.Add(prestamos);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }

            return View();
        }

        // POST: /Prestamos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,CuentaMonetariaSaldo,CuentaMonetariaId,Descripcion,Monto")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestamo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CuentaMonetariaId = new SelectList(db.CuentaMonetarias, "Id", "NombreCuenta", prestamo.CuentaMonetariaId);
            return View(prestamo);
        }

        // GET: /Prestamos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamoes.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // POST: /Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prestamo prestamo = db.Prestamoes.Find(id);
            db.Prestamoes.Remove(prestamo);
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
