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
    [RoutePrefix("api/Programmations")]
    public class ProgrammationsController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();

        //// GET: api/Programmations
        //public IQueryable<Programmation> GetProgrammations()
        //{
        //    return db.Programmations;
        //}

       
        [HttpGet]
        public IQueryable<ProgrammationDTO> GetProgrammations() {
            var programmmations = db.Programmations.Select(a =>
               new ProgrammationDTO()
               {
                   ArtisteId = a.ArtisteId,
                   ProgrammationId = a.ProgrammationId,
                   ProgrammationName = a.ProgrammationName,
                   FestivalId = a.FestivalId,
                   SceneId = a.SceneId,
                   Date =a.Date,
                   Duration =a.Duration
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

            
            var programmations =  db.Programmations.Where(a => a.FestivalId == festivalId).Select(a => new ProgrammationDTO()
            {
                ArtisteId = a.ArtisteId,
                FestivalId = a.FestivalId,
                ProgrammationId = a.ProgrammationId,
                ProgrammationName = a.ProgrammationName,
                SceneId = a.SceneId,
                Date = a.Date,
                Duration = a.Duration
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
                FestivalId = programmation.FestivalId,
                ProgrammationName = programmation.ProgrammationName,
                ArtisteId = programmation.ArtisteId,
                SceneId = programmation.SceneId,
                Date = programmation.Date,
                Duration = programmation.Duration
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