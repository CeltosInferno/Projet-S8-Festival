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
    public class ScenesController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();

        // GET: api/Scenes
        public IQueryable<SceneDTO> GetScenes()
        {
            var scenes = db.Scenes.Select(a => new SceneDTO()
            {
                SceneId = a.SceneId,
                Capacity = a.Capacity,
                SceneName = a.SceneName,
                Programmations = a.Programmations.Select(b => new ProgrammationDTO()
                 {
                     ArtisteId = b.ArtisteId,
                     ProgrammationId = b.ProgrammationId,
                     ProgrammationName = b.ProgrammationName
                 })
            });
            return scenes;
        }

        // GET: api/Scenes/5
        [ResponseType(typeof(Scene))]
        public async Task<IHttpActionResult> GetScene(int id)
        {
            //Scene scene = await db.Scenes.FindAsync(id);
            var scene = await (from a in db.Scenes
                               where a.SceneId == id
                               select new SceneDTO()
                               {
                                   Capacity = a.Capacity,
                                   SceneId = a.SceneId,
                                   SceneName = a.SceneName,
                                   Programmations = a.Programmations.Select(b => new ProgrammationDTO()
                                   {
                                       ArtisteId = b.ArtisteId,
                                       ProgrammationId = b.ProgrammationId,
                                       ProgrammationName = b.ProgrammationName
                                   })
                               }).FirstOrDefaultAsync();

            if (scene == null)
            {
                return NotFound();
            }

            return Ok(scene);
        }

        // PUT: api/Scenes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutScene(int id, Scene scene)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scene.SceneId)
            {
                return BadRequest();
            }

            db.Entry(scene).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SceneExists(id))
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

        // POST: api/Scenes
        [ResponseType(typeof(Scene))]
        public async Task<IHttpActionResult> PostScene(Scene scene)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Scenes.Add(scene);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = scene.SceneId }, scene);
        }

        // DELETE: api/Scenes/5
        [ResponseType(typeof(Scene))]
        public async Task<IHttpActionResult> DeleteScene(int id)
        {
            Scene scene = await db.Scenes.FindAsync(id);
            if (scene == null)
            {
                return NotFound();
            }

            db.Scenes.Remove(scene);
            await db.SaveChangesAsync();

            return Ok(scene);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SceneExists(int id)
        {
            return db.Scenes.Count(e => e.SceneId == id) > 0;
        }
    }
}