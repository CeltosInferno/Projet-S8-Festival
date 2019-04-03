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

namespace APIFestival.Controllers
{
    public class ArtistesController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();

        // GET: api/Artistes
        public IQueryable<ArtisteDTO> GetArtistes()
        {

            var artistes = from a in db.Artistes
                           select new ArtisteDTO()
                           {
                               Id = a.Id,
                               Comment = a.Comment,
                               FirstName=a.FirstName, LastName= a.LastName , MusicExtract=a.MusicExtract,
                               Nationality= a.Nationality,
                               Photo= a.Photo,
                               //Programmations= a.Programmations,
                               Style= a.Style

                           };
                           
            return artistes;
        }

        // GET: api/Artistes/5
        [ResponseType(typeof(Artiste))]
        public async Task<IHttpActionResult> GetArtiste(int id)
        {
            Artiste artiste = await db.Artistes.FindAsync(id);
            if (artiste == null)
            {
                return NotFound();
            }

            return Ok(artiste);
        }

        // PUT: api/Artistes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutArtiste(int id, Artiste artiste)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artiste.Id)
            {
                return BadRequest();
            }

            db.Entry(artiste).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
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
        public async Task<IHttpActionResult> PostArtiste(Artiste artiste)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Artistes.Add(artiste);
            await db.SaveChangesAsync();

            db.Entry(artiste).Reference(x => x.Programmation).Load();

            var dto = new ArtisteDTO()
            {
                Id = artiste.Id,
                Comment = artiste.Comment,
                FirstName = artiste.FirstName,
                ProgrammationId = artiste.Programmation.ProgrammationId,
                LastName = artiste.LastName,
                MusicExtract = artiste.MusicExtract,
                Nationality = artiste.Nationality,
                Photo = artiste.Photo,
                Style = artiste.Style
            };

            return CreatedAtRoute("DefaultApi", new { id = artiste.Id }, dto);
        }

        // DELETE: api/Artistes/5
        [ResponseType(typeof(Artiste))]
        public async Task<IHttpActionResult> DeleteArtiste(int id)
        {
            Artiste artiste = await db.Artistes.FindAsync(id);
            if (artiste == null)
            {
                return NotFound();
            }

            db.Artistes.Remove(artiste);
            await db.SaveChangesAsync();

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
            return db.Artistes.Count(e => e.Id == id) > 0;
        }
    }
}