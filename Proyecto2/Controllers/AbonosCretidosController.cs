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
    public class AbonosCretidosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/AbonosCretidos
        public IQueryable<AbonoDebito> GetAbonoDebitoes()
        {
            return db.AbonoDebitoes;
        }

        // GET api/AbonosCretidos/5
        [ResponseType(typeof(AbonoDebito))]
        public IHttpActionResult GetAbonoDebito(int id)
        {
            AbonoDebito abonodebito = db.AbonoDebitoes.Find(id);
            if (abonodebito == null)
            {
                return NotFound();
            }

            return Ok(abonodebito);
        }

        // PUT api/AbonosCretidos/5
        public IHttpActionResult PutAbonoDebito(int id, AbonoDebito abonodebito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != abonodebito.Id)
            {
                return BadRequest();
            }

            db.Entry(abonodebito).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbonoDebitoExists(id))
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

        // POST api/AbonosCretidos
        [ResponseType(typeof(AbonoDebito))]
        public IHttpActionResult PostAbonoDebito(AbonoDebito abonodebito)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AbonoDebitoes.Add(abonodebito);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = abonodebito.Id }, abonodebito);
        }

        // DELETE api/AbonosCretidos/5
        [ResponseType(typeof(AbonoDebito))]
        public IHttpActionResult DeleteAbonoDebito(int id)
        {
            AbonoDebito abonodebito = db.AbonoDebitoes.Find(id);
            if (abonodebito == null)
            {
                return NotFound();
            }

            db.AbonoDebitoes.Remove(abonodebito);
            db.SaveChanges();

            return Ok(abonodebito);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AbonoDebitoExists(int id)
        {
            return db.AbonoDebitoes.Count(e => e.Id == id) > 0;
        }
    }
}