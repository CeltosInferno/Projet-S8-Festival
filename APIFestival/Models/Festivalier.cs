using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Festivalier : Personne
    {
        // déclaration des attributs
        public String Genre { get; set; }
        public DateTime? DateNaissance { get; set; }
        public int Telephone { get; set; }

        // déclaration du constructeur
        public Festivalier(String genre, DateTime? dateNaissance, int telephone)
        {
            Genre = genre;
            DateNaissance = dateNaissance;
            Telephone = telephone;
        }
    }
}