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
    public class CuentasAhorrosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/CuentasAhorros
        public IQueryable<CuentaAhorro> GetCuentaAhorroes()
        {
            return db.CuentaAhorroes;
        }

        // GET api/CuentasAhorros/5
        [ResponseType(typeof(CuentaAhorro))]
        public IHttpActionResult GetCuentaAhorro(int id)
        {
            CuentaAhorro cuentaahorro = db.CuentaAhorroes.Find(id);
            if (cuentaahorro == null)
            {
                return NotFound();
            }

            return Ok(cuentaahorro);
        }

        // PUT api/CuentasAhorros/5
        public IHttpActionResult PutCuentaAhorro(int id, CuentaAhorro cuentaahorro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuentaahorro.Id)
            {
                return BadRequest();
            }

            db.Entry(cuentaahorro).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaAhorroExists(id))
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

        // POST api/CuentasAhorros
        [ResponseType(typeof(CuentaAhorro))]
        public IHttpActionResult PostCuentaAhorro(CuentaAhorro cuentaahorro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CuentaAhorroes.Add(cuentaahorro);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cuentaahorro.Id }, cuentaahorro);
        }

        // DELETE api/CuentasAhorros/5
        [ResponseType(typeof(CuentaAhorro))]
        public IHttpActionResult DeleteCuentaAhorro(int id)
        {
            CuentaAhorro cuentaahorro = db.CuentaAhorroes.Find(id);
            if (cuentaahorro == null)
            {
                return NotFound();
            }

            db.CuentaAhorroes.Remove(cuentaahorro);
            db.SaveChanges();

            return Ok(cuentaahorro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CuentaAhorroExists(int id)
        {
            return db.CuentaAhorroes.Count(e => e.Id == id) > 0;
        }
    }
}