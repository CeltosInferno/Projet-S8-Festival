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
    [RoutePrefix("api/Festivals")]
    public class FestivalsController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();

        // GET: api/Festivals
        [HttpGet]
        public IQueryable<FestivalDTO> GetFestivals()
        {
            var festivals = db.Festivals.Select(a => new FestivalDTO()
            {
                Description = a.Description,
                Name = a.Name,
                EndDate = a.EndDate,
                StartDate = a.StartDate,
                Id = a.Id,
                LieuName = a.LieuName,
                PostalCode = a.PostalCode,
                IsInscription = a.IsInscription,
                IsPublication = a.IsPublication,
                OrganisateurId = a.OrganisateurId,
                ProgrammationsList = a.Programmations.Select(b => new ProgrammationDTO()
                {
                    ArtisteId = b.ArtisteId,
                    ProgrammationId = b.ProgrammationId,
                    ProgrammationName = b.ProgrammationName,
                    FestivalId = b.FestivalId,
                    SceneId = b.SceneId
                })

            });
            return festivals;
        }

        // GET: api/Festivals/org/5
        [HttpGet]
        [Route("org/{organisateurId:int}")]
        public IQueryable<FestivalDTO> GetFestivalsByOrganizer(int organisateurId)
        {
            var festivals = db.Festivals.Where( a => a.OrganisateurId == organisateurId).Select(a => new FestivalDTO()
            {
                Description = a.Description,
                Name = a.Name,
                EndDate = a.EndDate,
                StartDate = a.StartDate,
                Id = a.Id,
                LieuName = a.LieuName,
                PostalCode = a.PostalCode,
                IsInscription = a.IsInscription,
                IsPublication = a.IsPublication,
                OrganisateurId = a.OrganisateurId,
                ProgrammationsList = a.Programmations.Select(b => new ProgrammationDTO()
                {
                    ArtisteId = b.ArtisteId,
                    ProgrammationId = b.ProgrammationId,
                    ProgrammationName = b.ProgrammationName,
                    FestivalId = b.FestivalId,
                    SceneId = b.SceneId
                })

            });
            return festivals;
        }

        // GET: api/Festivals/5
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(FestivalDTO))]
        public async Task<IHttpActionResult> GetFestival(int id)
        {
            var festival = await (from a in db.Festivals
                                  where a.Id == id
                                  select new FestivalDTO()
                                  {
                                      Description = a.Description,
                                      Name = a.Name,
                                      EndDate = a.EndDate,
                                      StartDate = a.StartDate,
                                      Id = a.Id,
                                      LieuName = a.LieuName,
                                      PostalCode = a.PostalCode,
                                      IsInscription = a.IsInscription,
                                      IsPublication = a.IsPublication
                                  }).FirstOrDefaultAsync();
              
            if (festival == null)
            {
                return NotFound();
            }

            return Ok(festival);
        }
        // GET: api/Festivals/5
        [HttpGet]
        [Route("{name}")]
        [ResponseType(typeof(FestivalDTO))]
        public async Task<IHttpActionResult> GetFestivalByName(string name)
        {
            var festival = await (from a in db.Festivals
                                  where a.Name == name
                                  select new FestivalDTO()
                                  {
                                      Description = a.Description,
                                      Name = a.Name,
                                      EndDate = a.EndDate,
                                      StartDate = a.StartDate,
                                      Id = a.Id,
                                      LieuName = a.LieuName,
                                      PostalCode = a.PostalCode,
                                      IsInscription = a.IsInscription,
                                      IsPublication = a.IsPublication
                                  }).FirstOrDefaultAsync();
            if (festival == null)
            {
                return NotFound();
            }

            return Ok(festival);
        }

        // PUT: api/Festivals/5
        [HttpPut]
        [Route("{id:int}")]
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
        [ResponseType(typeof(FestivalDTO))]
        public async Task<IHttpActionResult> PostFestival([FromBody]Festival festival)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.Festivals.Add(festival);
            await db.SaveChangesAsync();

            //db.Entry(artiste).Reference(x => x.Programmations).Load();

            var dto = new FestivalDTO()
            {
                Id = festival.Id,
                Description = festival.Description,
                EndDate = festival.EndDate,
                Name = festival.Name,
                StartDate = festival.StartDate,
                LieuName = festival.LieuName,
                PostalCode = festival.PostalCode,
                IsInscription = festival.IsInscription,
                IsPublication = festival.IsPublication
            };

            return CreatedAtRoute("DefaultApi", new { id = festival.Id }, dto);
        }

        // DELETE: api/Festivals/5
        [HttpDelete]
        [Route("{id:int}")]
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