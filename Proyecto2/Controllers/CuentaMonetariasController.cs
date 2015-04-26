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
    public class CuentaMonetariasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/CuentaMonetarias
        public IQueryable<CuentaMonetaria> GetCuentaMonetarias()
        {
            return db.CuentaMonetarias;
        }

        // GET api/CuentaMonetarias/5
        [ResponseType(typeof(CuentaMonetaria))]
        public IHttpActionResult GetCuentaMonetaria(int id)
        {
            CuentaMonetaria cuentamonetaria = db.CuentaMonetarias.Find(id);
            if (cuentamonetaria == null)
            {
                return NotFound();
            }

            return Ok(cuentamonetaria);
        }

        // PUT api/CuentaMonetarias/5
        public IHttpActionResult PutCuentaMonetaria(int id, CuentaMonetaria cuentamonetaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuentamonetaria.Id)
            {
                return BadRequest();
            }

            db.Entry(cuentamonetaria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaMonetariaExists(id))
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

        // POST api/CuentaMonetarias
        [ResponseType(typeof(CuentaMonetaria))]
        public IHttpActionResult PostCuentaMonetaria(CuentaMonetaria cuentamonetaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CuentaMonetarias.Add(cuentamonetaria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cuentamonetaria.Id }, cuentamonetaria);
        }

        // DELETE api/CuentaMonetarias/5
        [ResponseType(typeof(CuentaMonetaria))]
        public IHttpActionResult DeleteCuentaMonetaria(int id)
        {
            CuentaMonetaria cuentamonetaria = db.CuentaMonetarias.Find(id);
            if (cuentamonetaria == null)
            {
                return NotFound();
            }

            db.CuentaMonetarias.Remove(cuentamonetaria);
            db.SaveChanges();

            return Ok(cuentamonetaria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CuentaMonetariaExists(int id)
        {
            return db.CuentaMonetarias.Count(e => e.Id == id) > 0;
        }
    }
}