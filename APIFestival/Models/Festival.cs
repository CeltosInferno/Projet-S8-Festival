using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIFestival.Models
{
    public class Festival
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime? DateDebut { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateFin { get; set; }
        public string Lieu { get; set; }
        public int CodePostal { get; set; }

        // ajouter 2 fonctions  clôturer les inscriptions et clôturer la publication 
        public bool IsInscription { get; set; }
        public bool IsPublication { get; set; }
        // relier avec l'organisateur
        [ForeignKey("Organisateur")]
        public int OrganisateurId { get; set; }
        //ajouter nombre de place et coût
        public int  UserId { get; set; }
        public float Prix { get; set; }

        public virtual ICollection<Programmation> Programmations { get; set; }
        public virtual Organisateur Organisateur { get; set; }
    }
}