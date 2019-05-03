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
    public class SelectionsController : ApiController
    {
        private FestivalApiContext db = new FestivalApiContext();

        // GET: api/Selections
        public IQueryable<Selection> GetSelections()
        {
            return db.Selections;
        }

        // GET: api/Selections/5
        [ResponseType(typeof(Selection))]
        public IHttpActionResult GetSelection(int id)
        {
            Selection selection = db.Selections.Find(id);
            if (selection == null)
            {
                return NotFound();
            }

            return Ok(selection);
        }

        // PUT: api/Selections/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSelection(int id, Selection selection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != selection.SelectionId)
            {
                return BadRequest();
            }

            db.Entry(selection).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SelectionExists(id))
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

        // POST: api/Selections
        [ResponseType(typeof(Selection))]
        public IHttpActionResult PostSelection(Selection selection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Selections.Add(selection);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = selection.SelectionId }, selection);
        }

        // DELETE: api/Selections/5
        [ResponseType(typeof(Selection))]
        public IHttpActionResult DeleteSelection(int id)
        {
            Selection selection = db.Selections.Find(id);
            if (selection == null)
            {
                return NotFound();
            }

            db.Selections.Remove(selection);
            db.SaveChanges();

            return Ok(selection);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SelectionExists(int id)
        {
            return db.Selections.Count(e => e.SelectionId == id) > 0;
        }
    }
}