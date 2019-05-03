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
using FestivalApi.Models;

namespace FestivalApi.Controllers
{
    public class OrganisateursController : ApiController
    {
        private FestivalApiContext db = new FestivalApiContext();

        // GET: api/Organisateurs
        public IQueryable<Organisateur> GetOrganisateurs()
        {
            return db.Organisateurs;
        }

        // GET: api/Organisateurs/5
        [ResponseType(typeof(Organisateur))]
        public IHttpActionResult GetOrganisateur(int id)
        {
            Organisateur organisateur = db.Organisateurs.Find(id);
            if (organisateur == null)
            {
                return NotFound();
            }

            return Ok(organisateur);
        }

        // PUT: api/Organisateurs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrganisateur(int id, Organisateur organisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organisateur.Id)
            {
                return BadRequest();
            }

            db.Entry(organisateur).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganisateurExists(id))
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

        // POST: api/Organisateurs
        [ResponseType(typeof(Organisateur))]
        public IHttpActionResult PostOrganisateur(Organisateur organisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Organisateurs.Add(organisateur);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = organisateur.Id }, organisateur);
        }

        // DELETE: api/Organisateurs/5
        [ResponseType(typeof(Organisateur))]
        public IHttpActionResult DeleteOrganisateur(int id)
        {
            Organisateur organisateur = db.Organisateurs.Find(id);
            if (organisateur == null)
            {
                return NotFound();
            }

            db.Organisateurs.Remove(organisateur);
            db.SaveChanges();

            return Ok(organisateur);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganisateurExists(int id)
        {
            return db.Organisateurs.Count(e => e.Id == id) > 0;
        }
    }
}