using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace APIFestival.Models
{
    public class Festival
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        public string LieuName { get; set; }
        public int PostalCode { get; set; }

        // ajouter 2 fonctions  clôturer les inscriptions et clôturer la publication 
        public bool IsInscription { get; set; }
        public bool IsPublication { get; set; }


        public virtual ICollection<Programmation> Programmations { get; set; }
    }
}