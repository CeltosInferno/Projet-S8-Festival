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
using System.Linq.Expressions;
using APIFestival.Models.WEB;

namespace APIFestival.Controllers
{
    [RoutePrefix("api/Artistes")]
    public class ArtistesController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();


       
        // GET: api/Artistes
        /// <summary>
        /// Afficher tous les artistes
        /// </summary>
        /// <returns>artistes</returns>
        /// 
        [HttpGet]
        public IQueryable<ArtisteDTO> GetArtistes()
        {

            var artistes = db.Artistes.Select(a =>  new ArtisteDTO()
                {
                    ArtisteID = a.ArtisteID,
                    Comment = a.Comment,
                    ArtisteNom = a.ArtisteNom,
                    MusicExtract = a.MusicExtract,
                    Nationality = a.Nationality,
                    Photo = a.Photo,
                    Style = a.Style,
                    Programmations = a.Programmations.Select(b => new ProgrammationDTO()
                        {
                            ArtisteID = b.ArtisteID,
                            ProgrammationId = b.ProgrammationId,
                            ProgrammationName = b.ProgrammationName
                        })
                });
            return artistes;              
           // return db.Artistes.Include(a => a.Programmations).Select(AsArtisteDto);
        }
        // GET: api/Artistes/org
        [HttpGet]
        [Route("org")]
        public IQueryable<ArtisteWEB> GetArtistes2()
        {

            var artistes = db.Artistes.Select(a => new ArtisteWEB()
            {
                ArtisteID = a.ArtisteID,
                Comment = a.Comment,
                ArtisteNom = a.ArtisteNom,
                MusicExtract = a.MusicExtract,
                Nationality = a.Nationality,
                Photo = a.Photo,
                Style = a.Style
               
            });
            return artistes;
            // return db.Artistes.Include(a => a.Programmations).Select(AsArtisteDto);
        }

        // GET: api/Artistes/5
        [HttpGet]
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
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutArtiste(int id, Artiste artiste)
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
        [HttpPost]
        [ResponseType(typeof(ArtisteDTO))]
        public async Task<IHttpActionResult> PostArtiste(Artiste artiste)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Artistes.Add(artiste);
            await db.SaveChangesAsync();

            //db.Entry(artiste).Reference(x => x.Programmations).Load();

            var dto = new ArtisteDTO()
            {
                ArtisteID = artiste.ArtisteID,
                Comment = artiste.Comment,
                ArtisteNom = artiste.ArtisteNom,
                //ProgrammationId = artiste.Programmation.ProgrammationId,
                MusicExtract = artiste.MusicExtract,
                Nationality = artiste.Nationality,
                Photo = artiste.Photo,
                Style = artiste.Style
            };

            return CreatedAtRoute("DefaultApi", new { id = artiste.ArtisteID }, dto);
        }

        // DELETE: api/Artistes/5
        [HttpDelete]
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
            return db.Artistes.Count(e => e.ArtisteID == id) > 0;
        }
    }
}