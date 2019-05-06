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
using APIFestival.Models.DTO;
using APIFestival.Models.WEB;

namespace APIFestival.Controllers
{
    [RoutePrefix("api/Interets")]
    public class InteretsController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();

        // GET: api/Interets/org
        [HttpGet]
        [Route("org")]
        public IQueryable<InteretWEB> GetInterets()
        {

            var interets = db.Interets.Select(a => new InteretWEB()
            {
                Id = a.Id,
                FestivalId = a.FestivalId,
                Interesser = a.Interesser,
                Festival = new FestivalWEB()
                {
                    Id = a.Festival.Id,
                    Nom = a.Festival.Nom,
                    CodePostal = a.Festival.CodePostal,
                    Lieu = a.Festival.Lieu,
                    Description = a.Festival.Description,
                    DateDebut = a.Festival.DateDebut,
                    DateFin = a.Festival.DateFin,
                    Prix = a.Festival.Prix,
                    UserId = a.Festival.UserId
                }

            });
            return interets;
        }

        // GET: api/Interets/5
        [ResponseType(typeof(Interet))]
        public async Task<IHttpActionResult> GetInteret(int id)
        {
            Interet interet = await db.Interets.FindAsync(id);
            if (interet == null)
            {
                return NotFound();
            }

            return Ok(interet);
        }

        // PUT: api/Interets/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInteret(int id, Interet interet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != interet.Id)
            {
                return BadRequest();
            }

            db.Entry(interet).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InteretExists(id))
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

        // POST: api/Interets
        [ResponseType(typeof(Interet))]
        public async Task<IHttpActionResult> PostInteret(Interet interet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Interets.Add(interet);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = interet.Id }, interet);
        }

        // DELETE: api/Interets/5
        [ResponseType(typeof(Interet))]
        public async Task<IHttpActionResult> DeleteInteret(int id)
        {
            Interet interet = await db.Interets.FindAsync(id);
            if (interet == null)
            {
                return NotFound();
            }

            db.Interets.Remove(interet);
            await db.SaveChangesAsync();

            return Ok(interet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InteretExists(int id)
        {
            return db.Interets.Count(e => e.Id == id) > 0;
        }
    }
}