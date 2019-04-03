using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace APIFestival.Models
{
    public class Artiste:Personne
    {   
        //public Artiste()
        //{
        //    this.Programmations = new HashSet<Programmation>();
        //}
        
        public string Photo { get; set; }
        public string Style { get; set; }
        public string Comment { get; set; }
        public string Nationality { get; set; }
        public string MusicExtract { get; set; }

        [ForeignKey("Programmation")]
        public int ProgrammationId { get; set; }

        public virtual Programmation Programmation { get; set; }
    }
}