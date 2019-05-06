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
    [RoutePrefix("api/Scenes")]
    public class ScenesController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();

        // GET: api/Scenes
        [HttpGet]
        public IQueryable<SceneDTO> GetScenes()
        {
            var scenes = db.Scenes.Select(a => new SceneDTO()
            {
                Id = a.Id,
                Capacite = a.Capacite,
                Nom = a.Nom,
                Accessibilite=a.Accessibilite
               
            });
            return scenes;
        }
        [HttpGet]
        [Route("org")]
        // GET: api/Scenes
        public IQueryable<SceneWEB> GetScenes2()
        {
            var scenes = db.Scenes.Select(a => new SceneWEB()
            {
                Id = a.Id,
                Capacite = a.Capacite,
                Nom = a.Nom,
                Accessibilite = a.Accessibilite
            });
            return scenes;
        }

        // GET: api/Scenes/5
        [HttpGet]
        [ResponseType(typeof(Scene))]
        public async Task<IHttpActionResult> GetScene(int id)
        {
            //Scene scene = await db.Scenes.FindAsync(id);
            var scene = await (from a in db.Scenes
                               where a.Id == id
                               select new SceneDTO()
                               {
                                   Capacite = a.Capacite,
                                   Id = a.Id,
                                   Nom = a.Nom,
                                   Accessibilite =a.Accessibilite
                                   //Programmations = a.Programmations.Select(b => new ProgrammationDTO()
                                   //{
                                   //    ArtisteId = b.ArtisteId,
                                   //    ProgrammationId = b.ProgrammationId,
                                   //    ProgrammationName = b.ProgrammationName
                                   //})
                               }).FirstOrDefaultAsync();

            if (scene == null)
            {
                return NotFound();
            }

            return Ok(scene);
        }

        // PUT: api/Scenes/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutScene(int id, Scene scene)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scene.Id)
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
        [HttpPost]
        [ResponseType(typeof(SceneDTO))]
        public async Task<IHttpActionResult> PostScene(Scene scene)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Scenes.Add(scene);
            await db.SaveChangesAsync();
            var dto = new SceneDTO()
            {
                Capacite = scene.Capacite,
                Nom = scene.Nom,
                Accessibilite =scene.Accessibilite
            };
            return CreatedAtRoute("DefaultApi", new { id = scene.Id }, dto);
        }

        // DELETE: api/Scenes/5
        [HttpDelete]
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
            return db.Scenes.Count(e => e.Id == id) > 0;
        }
    }
}