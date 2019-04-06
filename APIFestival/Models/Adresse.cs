using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Adresse
    {
        // attributs
        public String CodePostal;
        public String Ville;
        public String Rue;
        public String Pays;

        // constructeur
        public Adresse(String codePostal, String ville, String rue, String pays)
        {
            CodePostal = codePostal;
            Ville = ville;
            Rue = rue;
            Pays = pays;
        }
    }
}