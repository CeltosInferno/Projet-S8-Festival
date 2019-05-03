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
    public class ScenesController : ApiController
    {
        private FestivalApiContext db = new FestivalApiContext();

        // GET: api/Scenes
        public IQueryable<Scene> GetScenes()
        {
            return db.Scenes;
        }

        // GET: api/Scenes/5
        [ResponseType(typeof(Scene))]
        public IHttpActionResult GetScene(int id)
        {
            Scene scene = db.Scenes.Find(id);
            if (scene == null)
            {
                return NotFound();
            }

            return Ok(scene);
        }

        // PUT: api/Scenes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutScene(int id, Scene scene)
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
                db.SaveChanges();
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
        public IHttpActionResult PostScene(Scene scene)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Scenes.Add(scene);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = scene.Id }, scene);
        }

        // DELETE: api/Scenes/5
        [ResponseType(typeof(Scene))]
        public IHttpActionResult DeleteScene(int id)
        {
            Scene scene = db.Scenes.Find(id);
            if (scene == null)
            {
                return NotFound();
            }

            db.Scenes.Remove(scene);
            db.SaveChanges();

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