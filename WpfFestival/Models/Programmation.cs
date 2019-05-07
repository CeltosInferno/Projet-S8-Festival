using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFestival.Models
{
    public class Programmation
    {
        public int ProgrammationId { get; set; }
        public string ProgrammationName { get; set; }
        public int FestivalID { get; set; }
        public int SceneID { get; set; }
        public int ArtisteID { get; set; }

        public DateTime DateFinConcert { get; set; }
        public DateTime DateDebutConcert { get; set; }
       // public int Duration { get; set; }
        public int OrganisateurID { get; set; }
    }
}
