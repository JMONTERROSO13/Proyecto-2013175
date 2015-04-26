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
    public class PersonaController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/Persona
        public IQueryable<Persona> GetPersonas()
        {
            return db.Personas;
        }

        // GET api/Persona/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult GetPersona(string id)
        {
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        // PUT api/Persona/5
        public IHttpActionResult PutPersona(string id, Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != persona.Dpi)
            {
                return BadRequest();
            }

            db.Entry(persona).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
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

        // POST api/Persona
        [ResponseType(typeof(Persona))]
        public IHttpActionResult PostPersona(Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Personas.Add(persona);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PersonaExists(persona.Dpi))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = persona.Dpi }, persona);
        }

        // DELETE api/Persona/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult DeletePersona(string id)
        {
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            db.Personas.Remove(persona);
            db.SaveChanges();

            return Ok(persona);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonaExists(string id)
        {
            return db.Personas.Count(e => e.Dpi == id) > 0;
        }
    }
}