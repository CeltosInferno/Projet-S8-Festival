using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Artiste
    {
        // déclaration des attributs
        public String Nom { get; set; }
        public String Style { get; set; }
        public String Pays { get; set; }
        public String Commentaire { get; set; }

        // déclaration du constructeur
        public Artiste(String nom, String style, String pays, String commentaire)
        {
            Nom = nom;
            Style = style;
            Pays = pays;
            Commentaire = commentaire;
        }
    }
}