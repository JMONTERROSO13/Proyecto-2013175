using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Proyecto2.Models;

namespace Proyecto2.Controllers
{
    public class aCuentasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/aCuentas
        public IQueryable<Cuenta> GetCuentas()
        {
            return db.Cuentas;
        }

        // GET api/aCuentas/5
        [ResponseType(typeof(Cuenta))]
        public IHttpActionResult GetCuenta(int id)
        {
            Cuenta cuenta = db.Cuentas.Find(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            return Ok(cuenta);
        }

        // PUT api/aCuentas/5
        public IHttpActionResult PutCuenta(int id, Cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuenta.Id)
            {
                return BadRequest();
            }

            db.Entry(cuenta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/aCuentas
        [ResponseType(typeof(Cuenta))]
        public IHttpActionResult PostCuenta(Cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cuentas.Add(cuenta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cuenta.Id }, cuenta);
        }

        // DELETE api/aCuentas/5
        [ResponseType(typeof(Cuenta))]
        public IHttpActionResult DeleteCuenta(int id)
        {
            Cuenta cuenta = db.Cuentas.Find(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            db.Cuentas.Remove(cuenta);
            db.SaveChanges();

            return Ok(cuenta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CuentaExists(int id)
        {
            return db.Cuentas.Count(e => e.Id == id) > 0;
        }
    }
}