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
    public class ProgrammationsController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();

        //// GET: api/Programmations
        //public IQueryable<Programmation> GetProgrammations()
        //{
        //    return db.Programmations;
        //}

       

        public IQueryable<ProgrammationDTO> GetProgrammations() {
            var programmmations = db.Programmations.Select(a =>
               new ProgrammationDTO()
               {
                   ArtisteId = a.ArtisteId,
                   ProgrammationId = a.ProgrammationId,
                   ProgrammationName = a.ProgrammationName,
                   FestivalId = a.FestivalId,
                   SceneId = a.SceneId
               });
            return programmmations;
        }

        // GET: api/Programmations/5
        [ResponseType(typeof(Programmation))]
        public async Task<IHttpActionResult> GetProgrammation(int id)
        {
            Programmation programmation = await db.Programmations.FindAsync(id);
            if (programmation == null)
            {
                return NotFound();
            }

            return Ok(programmation);
        }

        // GET: api/Programmations/5
        [ResponseType(typeof(Programmation))]
        public async Task<IHttpActionResult> GetProgrammationByName(string name)
        {
            

            Programmation programmation = await db.Programmations.FindAsync(name);
            if (programmation == null)
            {
                return NotFound();
            }

            return Ok(programmation);
        }

        // PUT: api/Programmations/5
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
                SceneId = programmation.SceneId
            };

            return CreatedAtRoute("DefaultApi", new { id = programmation.ProgrammationId }, dto);
        }

        // DELETE: api/Programmations/5
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