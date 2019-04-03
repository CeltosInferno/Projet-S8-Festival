using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIFestival.Models
{
    public class Festivalier:Personne
    {
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public int Telephone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}