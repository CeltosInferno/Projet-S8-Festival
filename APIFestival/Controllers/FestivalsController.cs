using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using APIFestival.Models;

namespace APIFestival.Controllers
{
    public class FestivalsController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();

        // GET: api/Festivals
        public IQueryable<Festival> GetFestivals()
        {
            return db.Festivals;
        }

        // GET: api/Festivals/5
        [ResponseType(typeof(Festival))]
        public async Task<IHttpActionResult> GetFestival(int id)
        {
            Festival festival = await db.Festivals.FindAsync(id);
            if (festival == null)
            {
                return NotFound();
            }

            return Ok(festival);
        }

        // PUT: api/Festivals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFestival(int id, Festival festival)
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
                await db.SaveChangesAsync();
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
        public async Task<IHttpActionResult> PostFestival(Festival festival)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Festivals.Add(festival);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = festival.Id }, festival);
        }

        // DELETE: api/Festivals/5
        [ResponseType(typeof(Festival))]
        public async Task<IHttpActionResult> DeleteFestival(int id)
        {
            Festival festival = await db.Festivals.FindAsync(id);
            if (festival == null)
            {
                return NotFound();
            }

            db.Festivals.Remove(festival);
            await db.SaveChangesAsync();

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