using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Festival.Models
{
    public class Inscription
    {
        public int ID { get; set;}
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime Naissance { get; set; }
        public string Email { get; set; }
        public string Mdp { get; set; }
        public string Genre { get; set; }
        public string Telephone { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Rue { get; set; }
        public string Pays { get; set; }
    }

    public class InscriptionDBContext : DbContext
    {
        DbSet<Inscription> Inscriptions { get; set;}
    }
}