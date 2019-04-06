using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Festival 
    {
        // déclaration des attributs
        public String Nom { get; set; }
        public String Description { get; set; }
        public String Debut { get; set; }
        public String Fin { get; set; }
        public String Ville { get; set; }
        public int CodePostal { get; set; }

        // déclaration du constructeur de la classe
        public Festival(String nom, String description, String debut, String fin, String ville, int codePostal)
        {
            Nom = nom;
            Description = description;
            Debut = debut;
            Fin = fin;
            CodePostal = codePostal;
            Ville = ville;
        }
    }
}