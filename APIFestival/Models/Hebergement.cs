using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Hebergement
    {
        // attributs
        public String Nom;
        public String Type;
        public String Lien;

        // constructeur
        public Hebergement(String nom, String type, String lien)
        {
            Nom = nom;
            Type = type;
            Lien = lien;
        }
    }
}