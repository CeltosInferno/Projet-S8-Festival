using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Horaire
    {
        public DateTime? Date { get; set; }
        public DateTime? Heure { get; set; }
        public int Durée { get; set; }
    }
}