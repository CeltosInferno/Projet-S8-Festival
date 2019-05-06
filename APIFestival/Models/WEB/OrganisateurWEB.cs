using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFestival.Models.WEB
{
    public class OrganisateurWEB
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; }
        public string Mdp { get; set; }
    }
}