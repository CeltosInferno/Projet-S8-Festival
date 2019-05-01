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

namespace APIFestival.Controllers
{
    public class ArtistesController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();


        //private static readonly Expression<Func<Artiste, ArtisteDTO>> AsArtisteDto =
        //    a => new ArtisteDTO
        //    {
        //        Id = a.ArtisteId,
        //        Comment = a.Comment,
        //        ArtisteName = a.ArtisteName,
        //        MusicExtract = a.MusicExtract,
        //        Nationality = a.Nationality,
        //        Photo = a.Photo,
        //        Style = a.Style,
               
        //    };
        // GET: api/Artistes
        /// <summary>
        /// Afficher tous les artistes
        /// </summary>
        /// <returns>artistes</returns>
        public IQueryable<ArtisteDTO> GetArtistes()
        {

            var artistes = db.Artistes.Select(a =>  new ArtisteDTO()
                {
                    ArtisteId = a.ArtisteId,
                    Comment = a.Comment,
                    ArtisteName = a.ArtisteName,
                    MusicExtract = a.MusicExtract,
                    Nationality = a.Nationality,
                    Photo = a.Photo,
                    Style = a.Style,
                    Programmations = a.Programmations.Select(b => new ProgrammationDTO()
                        {
                            ArtisteId = b.ArtisteId,
                            ProgrammationId = b.ProgrammationId,
                            ProgrammationName = b.ProgrammationName
                        })
                });



            return artistes;              
           // return db.Artistes.Include(a => a.Programmations).Select(AsArtisteDto);
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

            if (id != artiste.ArtisteId)
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
                ArtisteId = artiste.ArtisteId,
                Comment = artiste.Comment,
                ArtisteName = artiste.ArtisteName,
                //ProgrammationId = artiste.Programmation.ProgrammationId,
                MusicExtract = artiste.MusicExtract,
                Nationality = artiste.Nationality,
                Photo = artiste.Photo,
                Style = artiste.Style
            };

            return CreatedAtRoute("DefaultApi", new { id = artiste.ArtisteId }, dto);
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
            return db.Artistes.Count(e => e.ArtisteId == id) > 0;
        }
    }
}