using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FestivalApi.Models;

namespace FestivalApi.Controllers
{
    public class ProgrammationsController : ApiController
    {
        private FestivalApiContext db = new FestivalApiContext();

        // GET: api/Programmations
        public IQueryable<Programmation> GetProgrammations()
        {
            return db.Programmations;
        }

        // GET: api/Programmations/5
        [ResponseType(typeof(Programmation))]
        public IHttpActionResult GetProgrammation(int id)
        {
            Programmation programmation = db.Programmations.Find(id);
            if (programmation == null)
            {
                return NotFound();
            }

            return Ok(programmation);
        }

        // PUT: api/Programmations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProgrammation(int id, Programmation programmation)
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
                db.SaveChanges();
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
        [ResponseType(typeof(Programmation))]
        public IHttpActionResult PostProgrammation(Programmation programmation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Programmations.Add(programmation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = programmation.ProgrammationId }, programmation);
        }

        // DELETE: api/Programmations/5
        [ResponseType(typeof(Programmation))]
        public IHttpActionResult DeleteProgrammation(int id)
        {
            Programmation programmation = db.Programmations.Find(id);
            if (programmation == null)
            {
                return NotFound();
            }

            db.Programmations.Remove(programmation);
            db.SaveChanges();

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