using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FestivalApi.Models
{
    public class Festivalier
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set;}
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        public DateTime Naissance { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Mdp { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public string CodePostal { get; set; }
        [Required]
        public string Ville { get; set; }
        [Required]
        public string Rue { get; set; }
        [Required]
        public string Pays { get; set; }

        [ForeignKey("Festival")]
        public int FestivalId { get; set; }
        public virtual Festival Festival { get; set; }
    }
}