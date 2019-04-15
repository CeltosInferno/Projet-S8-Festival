using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIFestival.Models
{
    public class Festivalier:Personne
    {
        public string Sex { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
        public int Telephone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}