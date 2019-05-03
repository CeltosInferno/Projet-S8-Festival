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
    public class ArtistesController : ApiController
    {
        private FestivalApiContext db = new FestivalApiContext();

        // GET: api/Artistes
        public IQueryable<Artiste> GetArtistes()
        {
            return db.Artistes;
        }

        // GET: api/Artistes/5
        [ResponseType(typeof(Artiste))]
        public IHttpActionResult GetArtiste(int id)
        {
            Artiste artiste = db.Artistes.Find(id);
            if (artiste == null)
            {
                return NotFound();
            }

            return Ok(artiste);
        }

        // PUT: api/Artistes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArtiste(int id, Artiste artiste)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artiste.ArtisteID)
            {
                return BadRequest();
            }

            db.Entry(artiste).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtisteExists(id))
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

        // POST: api/Artistes
        [ResponseType(typeof(Artiste))]
        public IHttpActionResult PostArtiste(Artiste artiste)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Artistes.Add(artiste);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = artiste.ArtisteID }, artiste);
        }

        // DELETE: api/Artistes/5
        [ResponseType(typeof(Artiste))]
        public IHttpActionResult DeleteArtiste(int id)
        {
            Artiste artiste = db.Artistes.Find(id);
            if (artiste == null)
            {
                return NotFound();
            }

            db.Artistes.Remove(artiste);
            db.SaveChanges();

            return Ok(artiste);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArtisteExists(int id)
        {
            return db.Artistes.Count(e => e.ArtisteID == id) > 0;
        }
    }
}