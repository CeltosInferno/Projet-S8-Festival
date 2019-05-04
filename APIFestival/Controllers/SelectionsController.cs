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

namespace APIFestival.Controllers
{
    public class SelectionsController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();

        // GET: api/Selections
        public IQueryable<Selection> GetSelections()
        {
            return db.Selections;
        }

        // GET: api/Selections/5
        [ResponseType(typeof(Selection))]
        public async Task<IHttpActionResult> GetSelection(int id)
        {
            Selection selection = await db.Selections.FindAsync(id);
            if (selection == null)
            {
                return NotFound();
            }

            return Ok(selection);
        }

        // PUT: api/Selections/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSelection(int id, Selection selection)
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
                await db.SaveChangesAsync();
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
        public async Task<IHttpActionResult> PostSelection(Selection selection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Selections.Add(selection);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = selection.SelectionId }, selection);
        }

        // DELETE: api/Selections/5
        [ResponseType(typeof(Selection))]
        public async Task<IHttpActionResult> DeleteSelection(int id)
        {
            Selection selection = await db.Selections.FindAsync(id);
            if (selection == null)
            {
                return NotFound();
            }

            db.Selections.Remove(selection);
            await db.SaveChangesAsync();

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