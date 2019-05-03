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
    public class FestivalsController : ApiController
    {
        private FestivalApiContext db = new FestivalApiContext();

        // GET: api/Festivals
        public IQueryable<Festival> GetFestivals()
        {
            return db.Festivals;
        }

        // GET: api/Festivals/5
        [ResponseType(typeof(Festival))]
        public IHttpActionResult GetFestival(int id)
        {
            Festival festival = db.Festivals.Find(id);
            if (festival == null)
            {
                return NotFound();
            }

            return Ok(festival);
        }

        // PUT: api/Festivals/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFestival(int id, Festival festival)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != festival.Id)
            {
                return BadRequest();
            }

            db.Entry(festival).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FestivalExists(id))
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

        // POST: api/Festivals
        [ResponseType(typeof(Festival))]
        public IHttpActionResult PostFestival(Festival festival)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Festivals.Add(festival);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = festival.Id }, festival);
        }

        // DELETE: api/Festivals/5
        [ResponseType(typeof(Festival))]
        public IHttpActionResult DeleteFestival(int id)
        {
            Festival festival = db.Festivals.Find(id);
            if (festival == null)
            {
                return NotFound();
            }

            db.Festivals.Remove(festival);
            db.SaveChanges();

            return Ok(festival);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FestivalExists(int id)
        {
            return db.Festivals.Count(e => e.Id == id) > 0;
        }
    }
}