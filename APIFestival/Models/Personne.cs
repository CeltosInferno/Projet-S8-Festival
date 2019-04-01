using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Personne
    {
        // Declaration des attributs
        public int Id;
        public String Nom;
        public String Mail;
        public String Prenom;
        public String Password;

        // constructeur
        public Personne(String nom, String prenom, String password, String mail)
        {
            Nom = nom;
            Prenom = prenom;
            Password = password;
            Mail = mail;
        }
    }
}