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
    [RoutePrefix("api/Programmations")]
    public class ProgrammationsController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();

        //// GET: api/Programmations
        [HttpGet]
        public IQueryable<ProgrammationDTO> GetProgrammations() {
            var programmmations = db.Programmations.Select(a =>
               new ProgrammationDTO()
               {
                   ArtisteID = a.ArtisteID,
                   ProgrammationId = a.ProgrammationId,
                   ProgrammationName = a.ProgrammationName,
                   FestivalID = a.FestivalID,
                   SceneID = a.SceneID,
                   DateDebutConcert =a.DateDebutConcert, DateFinConcert=a.DateFinConcert, OrganisateurID=a.OrganisateurID
                   //Duration =a.Duration
               });
            return programmmations;
        }

        //// GET: api/Programmations/org
        [HttpGet]
        [Route("org")]
        public IQueryable<ProgrammationWEB> GetProgrammations2()
        {
            var programmmations = db.Programmations.Select(a =>
               new ProgrammationWEB()
               {
                   ArtisteID = a.ArtisteID,
                   ProgrammationId = a.ProgrammationId,
                   FestivalID = a.FestivalID,
                   SceneID = a.SceneID,
                   DateDebutConcert = a.DateDebutConcert,
                   DateFinConcert = a.DateFinConcert,
                   OrganisateurID = a.OrganisateurID,
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
                   },
                   Artiste = new ArtisteWEB()
                   {
                       ArtisteID = a.Artiste.ArtisteID,
                       ArtisteNom = a.Artiste.ArtisteNom,
                       Comment = a.Artiste.Comment,
                       MusicExtract = a.Artiste.MusicExtract,
                       Nationality = a.Artiste.Nationality,
                       Photo = a.Artiste.Photo,
                       Style = a.Artiste.Style
                   },
                   Organisateur = new OrganisateurWEB()
                   {
                       Id = a.Organisateur.Id,
                       Login = a.Organisateur.Login,
                       Mdp = a.Organisateur.Mdp,
                       Nom = a.Organisateur.Nom,
                       Prenom = a.Organisateur.Prenom
                   },
                   Scene = new SceneWEB()
                   {
                       Nom = a.Scene.Nom,
                       Id = a.Scene.Id,
                       Accessibilite = a.Scene.Accessibilite,
                       Capacite = a.Scene.Capacite
                   }
               });
            return programmmations;
        }

        //// GET: api/Programmations/5
        //[HttpGet]
        //[Route("{id:int}")]
        //[ResponseType(typeof(Programmation))]
        //public async Task<IHttpActionResult> GetProgrammation(int id)
        //{
        //    Programmation programmation = await db.Programmations.FindAsync(id);
        //    if (programmation == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(programmation);
        //}

        // GET: api/Programmations/5
        [HttpGet]
        [Route("{festivalId:int}")]
        
        public IQueryable<ProgrammationDTO> GetProgrammationByFestival(int festivalId)
        {

            
            var programmations =  db.Programmations.Where(a => a.FestivalID == festivalId).Select(a => new ProgrammationDTO()
            {
                ArtisteID = a.ArtisteID,
                FestivalID = a.FestivalID,
                ProgrammationId = a.ProgrammationId,
                ProgrammationName = a.ProgrammationName,
                SceneID = a.SceneID,
                DateDebutConcert = a.DateDebutConcert, DateFinConcert=a.DateFinConcert, OrganisateurID=a.OrganisateurID
                //Duration = a.Duration
            });
           
            return programmations;


        }

        // PUT: api/Programmations/5
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProgrammation(int id, Programmation programmation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != programmation.ProgrammationId)
            {
                return BadRequest();
            }

            db.Entry(programmation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgrammationExists(id))
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

        // POST: api/Programmations
        [HttpPost]
        [ResponseType(typeof(ProgrammationDTO))]
        public async Task<IHttpActionResult> PostProgrammation(Programmation programmation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Programmations.Add(programmation);
            await db.SaveChangesAsync();
            var dto = new ProgrammationDTO()
            {
                ProgrammationId = programmation.ProgrammationId,
                FestivalID = programmation.FestivalID,
                ProgrammationName = programmation.ProgrammationName,
                ArtisteID = programmation.ArtisteID,
                SceneID = programmation.SceneID,
                DateDebutConcert = programmation.DateDebutConcert, DateFinConcert=programmation.DateFinConcert, OrganisateurID=programmation.OrganisateurID
               // Duration = programmation.Duration
            };

            return CreatedAtRoute("DefaultApi", new { id = programmation.ProgrammationId }, dto);
        }

        [HttpPost]
        // POST: api/Programmations/CheckName?name=XXX
        [Route("CheckName")]
        [ResponseType(typeof(int))]
        public async Task<int> PostCheckProgrammationName(string name)
        {

            var org = await (from a in db.Programmations
                             where a.ProgrammationName == name
                             select new ProgrammationDTO()
                             {
                                 ProgrammationName = a.ProgrammationName
                             }).FirstOrDefaultAsync();
            if (org == null)
            {
                return 1; // nom n'existe pas, on peut créer programme
            }
            return 0;  //0 check no ok
        }

        // DELETE: api/Programmations/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(Programmation))]
        public async Task<IHttpActionResult> DeleteProgrammation(int id)
        {
            Programmation programmation = await db.Programmations.FindAsync(id);
            if (programmation == null)
            {
                return NotFound();
            }

            db.Programmations.Remove(programmation);
            await db.SaveChangesAsync();

            return Ok(programmation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProgrammationExists(int id)
        {
            return db.Programmations.Count(e => e.ProgrammationId == id) > 0;
        }
    }
}