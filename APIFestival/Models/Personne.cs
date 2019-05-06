using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace APIFestival.Models
{
    public class Personne
    {

        public int ID { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return Nom + ", " + Prenom;
            }
        }
    }
}