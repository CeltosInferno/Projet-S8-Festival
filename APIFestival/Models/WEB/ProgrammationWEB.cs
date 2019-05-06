using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFestival.Models.WEB
{
    public class ProgrammationWEB
    {
        public int ProgrammationId { get; set; }
        public int ArtisteID { get; set; }
        public int FestivalID { get; set; }
        public int SceneID { get; set; }

        public DateTime DateDebutConcert { get; set; }
        //public int Hour { get; set; }
        //public int Minute { get; set; }
        public DateTime DateFinConcert { get; set; }
        public int OrganisateurID { get; set; }

        public OrganisateurWEB Organisateur { get; set; }
        public SceneWEB Scene { get; set; }
        public ArtisteWEB Artiste { get; set; }
        public FestivalWEB Festival { get; set; }
    }
}