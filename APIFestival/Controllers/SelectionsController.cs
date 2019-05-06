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
    public class SelectionsController : ApiController
    {
        private APIFestivalContext db = new APIFestivalContext();

        // GET: api/Selections
        public IQueryable<SelectionWEB> GetSelections()
        {
            var selections = db.Selections.Select(a => new SelectionWEB()
            {
                FestivalierId = a.FestivalierId,
                PrimaireSecondaire = a.PrimaireSecondaire,
                ProgrammationId = a.ProgrammationId,
                SelectionId = a.SelectionId, Festivalier = new FestivalierWEB()
                {
                    ID = a.Festivalier.ID,
                    Nom = a.Festivalier.Nom,
                    Prenom = a.Festivalier.Prenom,
                    Naissance = a.Festivalier.Naissance,
                    Email = a.Festivalier.Email,
                    Mdp = a.Festivalier.Mdp,
                    Genre = a.Festivalier.Genre,
                    Telephone = a.Festivalier.Telephone,
                    CodePostal = a.Festivalier.CodePostal,
                    Ville = a.Festivalier.Ville,
                    Rue = a.Festivalier.Rue,
                    Pays = a.Festivalier.Pays,
                    FestivalId = a.Festivalier.FestivalId,
                    Festival = new FestivalWEB()
                    {
                        Id = a.Festivalier.Festival.Id,
                        Nom = a.Festivalier.Festival.Nom,
                        CodePostal = a.Festivalier.Festival.CodePostal,
                        Lieu = a.Festivalier.Festival.Lieu,
                        Description = a.Festivalier.Festival.Description,
                        DateDebut = a.Festivalier.Festival.DateDebut,
                        DateFin = a.Festivalier.Festival.DateFin,
                        Prix = a.Festivalier.Festival.Prix,
                        UserId = a.Festivalier.Festival.UserId
                    }
                }, Programmation = new ProgrammationWEB()
                {
                    ArtisteID = a.Programmation.ArtisteID,
                    ProgrammationId = a.Programmation.ProgrammationId,
                    FestivalID = a.Programmation.FestivalID,
                    SceneID = a.Programmation.SceneID,
                    DateDebutConcert = a.Programmation.DateDebutConcert,
                    DateFinConcert = a.Programmation.DateFinConcert,
                    OrganisateurID = a.Programmation.OrganisateurID,
                    Festival = new FestivalWEB()
                    {
                        Id = a.Programmation.Festival.Id,
                        Nom = a.Programmation.Festival.Nom,
                        CodePostal = a.Programmation.Festival.CodePostal,
                        Lieu = a.Programmation.Festival.Lieu,
                        Description = a.Programmation.Festival.Description,
                        DateDebut = a.Programmation.Festival.DateDebut,
                        DateFin = a.Programmation.Festival.DateFin,
                        Prix = a.Programmation.Festival.Prix,
                        UserId = a.Programmation.Festival.UserId
                    },
                    Artiste = new ArtisteWEB()
                    {
                        ArtisteID = a.Programmation.Artiste.ArtisteID,
                        ArtisteNom = a.Programmation.Artiste.ArtisteNom,
                        Comment = a.Programmation.Artiste.Comment,
                        MusicExtract = a.Programmation.Artiste.MusicExtract,
                        Nationality = a.Programmation.Artiste.Nationality,
                        Photo = a.Programmation.Artiste.Photo,
                        Style = a.Programmation.Artiste.Style
                    },
                    Organisateur = new OrganisateurWEB()
                    {
                        Id = a.Programmation.Organisateur.Id,
                        Login = a.Programmation.Organisateur.Login,
                        Mdp = a.Programmation.Organisateur.Mdp,
                        Nom = a.Programmation.Organisateur.Nom,
                        Prenom = a.Programmation.Organisateur.Prenom
                    },
                    Scene = new SceneWEB()
                    {
                        Nom = a.Programmation.Scene.Nom,
                        Id = a.Programmation.Scene.Id,
                        Accessibilite = a.Programmation.Scene.Accessibilite,
                        Capacite = a.Programmation.Scene.Capacite
                    }
                }
            });
            return selections;
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