using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Scene
    {
        // déclaration des attributs
        public String Nom { get; set; }
        public String Capacite { get; set; }
        public String Accessibilite { get; set; }

        // déclaration du constructeur
        public Scene(String nom, String capacite, String accessibilte)
        {
            Nom = nom;
            Capacite = capacite;
            Accessibilite = accessibilite;
        }
    }
}