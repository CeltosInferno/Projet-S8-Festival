using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFestival.Models.WEB
{
    public class FestivalWEB
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int CodePostal { get; set; }
        public string Lieu { get; set; }
        public string Description { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public double Prix { get; set; }

        public int UserId { get; set; }

        
    }
}