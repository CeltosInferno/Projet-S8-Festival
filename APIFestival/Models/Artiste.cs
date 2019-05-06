using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace APIFestival.Models
{
    public class Artiste
    {   
        public Artiste()
        {
            this.Programmations = new HashSet<Programmation>();
        }

        [Key]
        public int ArtisteID { get; set; }
        public string ArtisteNom { get; set; }
        public string Photo { get; set; }
        public string Style { get; set; }
        public string Comment { get; set; }
        public string Nationality { get; set; }
        public string MusicExtract { get; set; }

       

        public virtual ICollection<Programmation> Programmations { get; set; }
    }
}