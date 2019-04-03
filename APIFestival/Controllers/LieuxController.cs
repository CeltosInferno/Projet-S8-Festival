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
    public class LieuxController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();

        // GET: api/Lieux
        public IQueryable<Lieu> GetLieux()
        {
            return db.Lieux;
        }

        // GET: api/Lieux/5
        [ResponseType(typeof(Lieu))]
        public async Task<IHttpActionResult> GetLieu(int id)
        {
            Lieu lieu = await db.Lieux.FindAsync(id);
            if (lieu == null)
            {
                return NotFound();
            }

            return Ok(lieu);
        }

        // PUT: api/Lieux/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLieu(int id, Lieu lieu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lieu.FestivalId)
            {
                return BadRequest();
            }

            db.Entry(lieu).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LieuExists(id))
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

        // POST: api/Lieux
        [ResponseType(typeof(Lieu))]
        public async Task<IHttpActionResult> PostLieu(Lieu lieu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lieux.Add(lieu);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LieuExists(lieu.FestivalId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = lieu.FestivalId }, lieu);
        }

        // DELETE: api/Lieux/5
        [ResponseType(typeof(Lieu))]
        public async Task<IHttpActionResult> DeleteLieu(int id)
        {
            Lieu lieu = await db.Lieux.FindAsync(id);
            if (lieu == null)
            {
                return NotFound();
            }

            db.Lieux.Remove(lieu);
            await db.SaveChangesAsync();

            return Ok(lieu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LieuExists(int id)
        {
            return db.Lieux.Count(e => e.FestivalId == id) > 0;
        }
    }
}