﻿using System;
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
    [RoutePrefix("api/Organisateurs")]
    public class OrganisateursController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();

        // GET: api/Organisateurs
        [HttpGet]
        public IQueryable<Organisateur> GetOrganisateurs()
        {
            return db.Organisateurs;
        }

        //GET: api/Organisateurs/email
        [HttpGet]
        [Route("{email}")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> GetOrganisateur(string email)
        {
            var id = await (from a in db.Organisateurs
                             where a.Email == email
                             select new OrganisateurNameDTO()
                             {
                                 Id = a.Id
                             }).FirstOrDefaultAsync();
            if (id == null)
            {
                return NotFound();
            }

            return Ok(id);
        }

        //// GET: api/Organisateurs/5
        //[HttpGet]
        //[ResponseType(typeof(Organisateur))]
        //public async Task<IHttpActionResult> GetOrganisateurByName(int id)
        //{
        //    Organisateur organisateur = await db.Organisateurs.FindAsync(id);
        //    if (organisateur == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(organisateur);
        //}

        // PUT: api/Organisateurs/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOrganisateur(int id, Organisateur organisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organisateur.Id)
            {
                return BadRequest();
            }

            db.Entry(organisateur).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganisateurExists(id))
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
        //[HttpPost]
        //// POST: api/Organisateurs
        //[ResponseType(typeof(Organisateur))]
        //public async Task<IHttpActionResult> PostOrganisateur(Organisateur organisateur)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Organisateurs.Add(organisateur);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = organisateur.Id }, organisateur);
        //}
        [HttpPost]
        // POST: api/Organisateurs
        [ResponseType(typeof(int))]
        public async Task<int> CheckPassword(OrganisateurDTO organisateur)
        {

            var org = await (from a in db.Organisateurs
                             where a.Email == organisateur.Email
                             select new OrganisateurDTO()
                             {
                                 Email = a.Email,
                                 Id = a.Id,
                                 Password = a.Password
                             }).FirstOrDefaultAsync();
            if (org == null)
            {
                return 0; // 0 email n'existe pas
            }
            else if(org.Password.Equals(organisateur.Password))
            {
                return 1; //1  check ok
            }
            else { return -1; } //-1 check no ok
        }

        [HttpDelete]
        // DELETE: api/Organisateurs/5
        [ResponseType(typeof(Organisateur))]
        public async Task<IHttpActionResult> DeleteOrganisateur(int id)
        {
            Organisateur organisateur = await db.Organisateurs.FindAsync(id);
            if (organisateur == null)
            {
                return NotFound();
            }

            db.Organisateurs.Remove(organisateur);
            await db.SaveChangesAsync();

            return Ok(organisateur);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganisateurExists(int id)
        {
            return db.Organisateurs.Count(e => e.Id == id) > 0;
        }
    }
}