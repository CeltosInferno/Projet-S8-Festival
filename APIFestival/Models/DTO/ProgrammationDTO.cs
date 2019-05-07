using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFestival.Models.DTO
{
    public class ProgrammationDTO
    {
        public int ProgrammationId { get; set; }
        public string ProgrammationName { get; set; }
        public int ArtisteID { get; set; }
        public int FestivalID { get; set; }
        public int SceneID { get; set; }

        public DateTime DateDebutConcert { get; set; }
        //public int Hour { get; set; }
        //public int Minute { get; set; }
       // public int Duration { get; set; }
        public DateTime DateFinConcert { get; set; }
        public int OrganisateurID { get; set; }
       
    }
}