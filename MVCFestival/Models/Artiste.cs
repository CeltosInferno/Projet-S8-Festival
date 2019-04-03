using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFestival.Models
{
    public class Artiste:Personne
    {
        public string Photo { get; set; }
        public string Style { get; set; }
        public string Comment { get; set; }
        public string Nationality { get; set; }
        public string MusicExtract { get; set; }
    }
}