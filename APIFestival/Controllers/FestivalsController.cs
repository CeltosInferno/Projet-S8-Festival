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
                Nom = a.Nom,
                DateFin = a.DateFin,
                DateDebut = a.DateDebut,
                Id = a.Id,
                Lieu = a.Lieu,
                CodePostal = a.CodePostal,
                IsInscription = a.IsInscription,
                IsPublication = a.IsPublication,
                OrganisateurId = a.OrganisateurId,
                UserId = a.UserId,
                Prix = a.Prix,
                ProgrammationsList = a.Programmations.Select(b => new ProgrammationDTO()
                {
                    ArtisteID = b.ArtisteID,
                    ProgrammationId = b.ProgrammationId,
                    ProgrammationName = b.ProgrammationName,
                    FestivalID = b.FestivalID,
                    SceneID = b.SceneID
                })

            });
            return festivals;
        }

        // GET: api/Festivals/org
        [HttpGet]
        [Route("org")]
        public IQueryable<FestivalWEB> GetFestivals2()
        {
            var festivals = db.Festivals.Select(a => new FestivalWEB()
            {
                Description = a.Description,
                Nom = a.Nom,
                DateFin = a.DateFin,
                DateDebut = a.DateDebut,
                Id = a.Id,
                Lieu = a.Lieu,
                CodePostal = a.CodePostal,
                //IsInscription = a.IsInscription,
                //IsPublication = a.IsPublication,
                //OrganisateurId = a.OrganisateurId,
                UserId = a.UserId,
                Prix = a.Prix,
                

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
                Nom = a.Nom,
                DateFin = a.DateFin,
                DateDebut = a.DateDebut,
                Id = a.Id,
                Lieu = a.Lieu,
                CodePostal = a.CodePostal,
                IsInscription = a.IsInscription,
                IsPublication = a.IsPublication,
                OrganisateurId = a.OrganisateurId,
                UserId = a.UserId,
                Prix = a.Prix,
                ProgrammationsList = a.Programmations.Select(b => new ProgrammationDTO()
                {
                    ArtisteID = b.ArtisteID,
                    ProgrammationId = b.ProgrammationId,
                    ProgrammationName = b.ProgrammationName,
                    FestivalID = b.FestivalID,
                    SceneID = b.SceneID
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
                                      Nom = a.Nom,
                                      DateFin = a.DateFin,
                                      DateDebut = a.DateDebut,
                                      Id = a.Id,
                                      Lieu = a.Lieu,
                                      CodePostal = a.CodePostal,
                                      UserId = a.UserId,
                                      Prix = a.Prix,
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
                                  where a.Nom == name
                                  select new FestivalDTO()
                                  {
                                      Description = a.Description,
                                      Nom = a.Nom,
                                      DateFin = a.DateFin,
                                      DateDebut = a.DateDebut,
                                      Id = a.Id,
                                      Lieu = a.Lieu,
                                      CodePostal = a.CodePostal,
                                      UserId = a.UserId,
                                      Prix = a.Prix,
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
                DateFin = festival.DateFin,
                Nom = festival.Nom,
                DateDebut = festival.DateDebut,
                Lieu = festival.Lieu,
                CodePostal = festival.CodePostal,
                UserId = festival.UserId,
                Prix = festival.Prix,
                IsInscription = festival.IsInscription,
                IsPublication = festival.IsPublication
            };

            return CreatedAtRoute("DefaultApi", new { id = festival.Id }, dto);
        }

        [HttpPost]
        // POST: api/Festivals/CheckName?name=XXX
        [Route("CheckName")]
        [ResponseType(typeof(int))]
        public async Task<int> PostCheckFestivalName(string name)
        {

            var org = await (from a in db.Festivals
                             where a.Nom == name
                             select new FestivalDTO()
                             {
                                  Nom=a.Nom
                             }).FirstOrDefaultAsync();
            if (org == null)
            {
                return 1; // nom n'existe pas, on peut créer festival
            }
            return 0;  //0 check no ok
        }

        [HttpPost]
        // POST: api/Festivals/FestivalId?name=XXX
        [Route("FestivalId")]
        [ResponseType(typeof(int))]
        public async Task<int> GetFestivalId(string name)
        {

            var org = await (from a in db.Festivals
                             where a.Nom == name
                             select new FestivalDTO()
                             {
                                 Nom = a.Nom,
                                 OrganisateurId=a.OrganisateurId,
                                 Id=a.Id
                                 
                             }).FirstOrDefaultAsync();
            if (org == null)
            {
                return 0; // ne trouve pas
            }
            return org.Id;  //get festivalId by festival name
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