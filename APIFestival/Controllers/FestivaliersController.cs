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
    [RoutePrefix("api/Festivaliers")]
    public class FestivaliersController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();

        // GET: api/Festivaliers/org
        [HttpGet]
        [Route("org")]
        public IQueryable<FestivalierWEB> GetFestivaliers()
        {

            var f = db.Festivaliers.Select(a => new FestivalierWEB()
            {
                ID = a.ID,
                Nom = a.Nom,
                Prenom = a.Prenom,
                Naissance = a.Naissance,
                Email = a.Email,
                Mdp = a.Mdp,
                Genre = a.Genre,
                Telephone = a.Telephone,
                CodePostal = a.CodePostal,
                Ville = a.Ville,
                Rue = a.Rue,
                Pays = a.Pays,
                FestivalId = a.FestivalId,
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
            return f;
        }

        // GET: api/Festivaliers/5
        [ResponseType(typeof(Festivalier))]
        public async Task<IHttpActionResult> GetFestivalier(int id)
        {
            Festivalier festivalier = await db.Festivaliers.FindAsync(id);
            if (festivalier == null)
            {
                return NotFound();
            }

            return Ok(festivalier);
        }

        // PUT: api/Festivaliers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFestivalier(int id, Festivalier festivalier)
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
                await db.SaveChangesAsync();
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
        [ResponseType(typeof(FestivalierDTO))]
        public async Task<IHttpActionResult> PostArtiste(Festivalier festival)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Festivaliers.Add(festival);
            await db.SaveChangesAsync();

            var dto = new FestivalierDTO()
            {
                ID = festival.ID,
                Nom = festival.Nom,
                Prenom = festival.Prenom,
                Naissance = festival.Naissance,
                Email = festival.Email,
                Mdp = festival.Mdp,
                Genre = festival.Genre,
                Telephone = festival.Telephone,
                CodePostal = festival.CodePostal,
                Ville = festival.Ville,
                Rue = festival.Rue,
                Pays = festival.Pays,
                FestivalId = festival.FestivalId,
            };

            return CreatedAtRoute("DefaultApi", new { id = festival.ID }, dto);
        }

        // DELETE: api/Festivaliers/5
        [ResponseType(typeof(Festivalier))]
        public async Task<IHttpActionResult> DeleteFestivalier(int id)
        {
            Festivalier festivalier = await db.Festivaliers.FindAsync(id);
            if (festivalier == null)
            {
                return NotFound();
            }

            db.Festivaliers.Remove(festivalier);
            await db.SaveChangesAsync();

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