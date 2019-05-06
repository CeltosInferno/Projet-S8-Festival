﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFestival.Models.DTO
{
    public class FestivalierDTO
    {   
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Genre { get; set; }
        public DateTime Naissance { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Mdp { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Rue { get; set; }
        public string Pays { get; set; }

        public int FestivalId { get; set; }
    }
}