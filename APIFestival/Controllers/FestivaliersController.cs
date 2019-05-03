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
    public class FestivaliersController : ApiController
    {
        private FestivalApiContext db = new FestivalApiContext();

        // GET: api/Festivaliers
        public IQueryable<Festivalier> GetFestivaliers()
        {
            return db.Festivaliers;
        }

        // GET: api/Festivaliers/5
        [ResponseType(typeof(Festivalier))]
        public IHttpActionResult GetFestivalier(int id)
        {
            Festivalier festivalier = db.Festivaliers.Find(id);
            if (festivalier == null)
            {
                return NotFound();
            }

            return Ok(festivalier);
        }

        // PUT: api/Festivaliers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFestivalier(int id, Festivalier festivalier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != festivalier.ID)
            {
                return BadRequest();
            }

            db.Entry(festivalier).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FestivalierExists(id))
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

        // POST: api/Festivaliers
        [ResponseType(typeof(Festivalier))]
        public IHttpActionResult PostFestivalier(Festivalier festivalier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Festivaliers.Add(festivalier);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = festivalier.ID }, festivalier);
        }

        // DELETE: api/Festivaliers/5
        [ResponseType(typeof(Festivalier))]
        public IHttpActionResult DeleteFestivalier(int id)
        {
            Festivalier festivalier = db.Festivaliers.Find(id);
            if (festivalier == null)
            {
                return NotFound();
            }

            db.Festivaliers.Remove(festivalier);
            db.SaveChanges();

            return Ok(festivalier);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FestivalierExists(int id)
        {
            return db.Festivaliers.Count(e => e.ID == id) > 0;
        }
    }
}