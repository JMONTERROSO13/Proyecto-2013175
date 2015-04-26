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
    public class AbonosAhorrosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/AbonosAhorros
        public IQueryable<AbonoAhorro> GetAbonoAhorroes()
        {
            return db.AbonoAhorroes;
        }

        // GET api/AbonosAhorros/5
        [ResponseType(typeof(AbonoAhorro))]
        public IHttpActionResult GetAbonoAhorro(int id)
        {
            AbonoAhorro abonoahorro = db.AbonoAhorroes.Find(id);
            if (abonoahorro == null)
            {
                return NotFound();
            }

            return Ok(abonoahorro);
        }

        // PUT api/AbonosAhorros/5
        public IHttpActionResult PutAbonoAhorro(int id, AbonoAhorro abonoahorro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != abonoahorro.Id)
            {
                return BadRequest();
            }

            db.Entry(abonoahorro).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbonoAhorroExists(id))
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

        // POST api/AbonosAhorros
        [ResponseType(typeof(AbonoAhorro))]
        public IHttpActionResult PostAbonoAhorro(AbonoAhorro abonoahorro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AbonoAhorroes.Add(abonoahorro);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = abonoahorro.Id }, abonoahorro);
        }

        // DELETE api/AbonosAhorros/5
        [ResponseType(typeof(AbonoAhorro))]
        public IHttpActionResult DeleteAbonoAhorro(int id)
        {
            AbonoAhorro abonoahorro = db.AbonoAhorroes.Find(id);
            if (abonoahorro == null)
            {
                return NotFound();
            }

            db.AbonoAhorroes.Remove(abonoahorro);
            db.SaveChanges();

            return Ok(abonoahorro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AbonoAhorroExists(int id)
        {
            return db.AbonoAhorroes.Count(e => e.Id == id) > 0;
        }
    }
}