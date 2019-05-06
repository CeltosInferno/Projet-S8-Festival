using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFestival.Models.DTO
{
    public class ArtisteDTO
    {
        public int ArtisteID { get; set; }
        public string ArtisteNom { get; set; }
        public string Photo { get; set; }
        public string Style { get; set; }
        public string Comment { get; set; }
        public string Nationality { get; set; }
        public string MusicExtract { get; set; }
        public IEnumerable<ProgrammationDTO> Programmations { get; set; }
       //public string ProgrammationName { get; set; }


    }
}