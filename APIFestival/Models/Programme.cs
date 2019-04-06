using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Programme
    {
        // déclaration des attributs
        public String Nom { get; set; }

        // déclaration du constructeur
        public Programme(String nom)
        {
            Nom = nom;
        }
    }
}