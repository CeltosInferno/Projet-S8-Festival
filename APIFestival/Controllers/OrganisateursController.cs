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
    [RoutePrefix("api/Organisateurs")]
    public class OrganisateursController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();

        // GET: api/Organisateurs
        [HttpGet]
        public IQueryable<OrganisateurDTO> GetOrganisateurs()
        {

            var organisateurs = db.Organisateurs.Select(a => new OrganisateurDTO()
            {
                Id = a.Id,
                Login = a.Login,
                Mdp = a.Mdp,
                Nom = a.Nom,
                Prenom = a.Prenom
            });
            return organisateurs;
        }

        // GET: api/Organisateurs/org
        [HttpGet]
        [Route("org")]
        public IQueryable<OrganisateurWEB> GetOrganisateurs2()
        {

            var organisateurs = db.Organisateurs.Select(a => new OrganisateurWEB()
            {
                Id = a.Id,
                Login = a.Login,
                Mdp = a.Mdp,
                Nom = a.Nom,
                Prenom = a.Prenom
            });
            return organisateurs;
        }


        //GET: api/Organisateurs/email
        [HttpGet]
        [Route("{email}")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> GetOrganisateur(string email)
        {
            var id = await (from a in db.Organisateurs
                             where a.Login == email
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
        [ResponseType(typeof(Organisateur))]
        [Route("api/Organisateurs/Verification")]
        [HttpPost]
        public async Task<IHttpActionResult> Verification(Organisateur user1)
        {
            Organisateur user = await db.Organisateurs.Where(u => u.Login == user1.Login).FirstOrDefaultAsync();
            Console.WriteLine("Dans la ligne 26");
            if (user == null)
            {
                Console.WriteLine("Dans la ligne 29");
                return NotFound();
            }

            if (!user1.Mdp.Equals(user.Mdp))
            {
                Console.WriteLine("Dans la ligne 35");
                return BadRequest();
            }
            Console.WriteLine("Dans la ligne 38");

            user.Mdp = null;
            return Ok(user);
        }
        [HttpPost]
        // POST: api/Organisateurs
        [ResponseType(typeof(int))]
        public async Task<int> CheckPassword(OrganisateurDTO organisateur)
        {

            var org = await (from a in db.Organisateurs
                             where a.Login == organisateur.Login
                             select new OrganisateurDTO()
                             {
                                 Login = a.Login,
                                 Id = a.Id,
                                 Mdp = a.Mdp
                             }).FirstOrDefaultAsync();
            if (org == null)
            {
                return 0; // 0 email n'existe pas
            }
            else if(org.Mdp.Equals(organisateur.Mdp))
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